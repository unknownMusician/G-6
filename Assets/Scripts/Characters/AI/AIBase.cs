using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AIBase : CharacterBase {

    #region Properties

    protected AIState CurrentAIState {
        get => _currentAIState;
        set {
            var lastState = _currentAIState;
            if (_currentAIState == value)
                return;
            _currentAIState = value;

            // all ends
            switch (lastState) {
                case AIState.Aggressive:
                    OnAggressiveStateEnd();
                    break;
                case AIState.Calm:
                    OnCalmStateEnd();
                    break;
                case AIState.Suspicious:
                    OnSuspiciousStateEnd();
                    break;
                case AIState.LostTarget:
                    OnLostTargetStateEnd();
                    break;
                case AIState.LookAround:
                    OnLookAroundStateEnd();
                    break;
                case AIState.Busy:
                    OnBusyStateEnd();
                    break;
            }

            OnStateChanged();

            //all starts
            switch (_currentAIState) {
                case AIState.Aggressive:
                    OnAggressiveStateStart();
                    break;
                case AIState.Calm:
                    OnCalmStateStart();
                    break;
                case AIState.Suspicious:
                    OnSuspiciousStateStart();
                    break;
                case AIState.LostTarget:
                    OnLostTargetStateStart();
                    break;
                case AIState.LookAround:
                    OnLookAroundStateStart();
                    break;
                case AIState.Busy:
                    OnBusyStateStart();
                    break;
            }
        }
    }

    protected GameObject Enemy { get; set; }

    protected Vector2 LocalLookPos { get; set; } = Vector2.right;
    protected Vector2 WorldLookPos { get => (Vector2)transform.position + LocalLookPos; set => LocalLookPos = value - (Vector2)transform.position; }
    protected Vector2 EnemyPos => Enemy.transform.position;
    protected Vector2 EnemyLocalPos => EnemyPos - (Vector2)transform.position;
    protected Vector2 EnemyLastSeen { get; set; } = Vector2.zero;
    protected Vector2 LastPosition { get; set; } = Vector2.zero;
    protected Vector2 WantedMoveDir { get; set; } = Vector2.zero;

    protected float DistanceToEnemy => EnemyLocalPos.magnitude;
    protected float SpotScale { get => _spotScale; set => _spotScale = value > 1 ? 1 : (value < 0 ? 0 : value); }

    protected int BulletsClip => (Inventory.Weapon is Gun gun) ? gun.ActualClipBullets : 0;
    protected int BulletsPocket => (Inventory.Weapon is Gun gun) ? gun.ActualPocketBullets : 0;

    protected RaycastHit2D SensorRightRaw => Physics2D.Raycast(transform.position, Vector2.right, viewDistance, canSeeLayerMask);
    protected RaycastHit2D SensorLeftRaw => Physics2D.Raycast(transform.position, Vector2.left, viewDistance, canSeeLayerMask);
    protected RaycastHit2D SensorBottomRaw => Physics2D.Raycast(transform.position, Vector2.down, viewDistance, canSeeLayerMask);
    protected float SensorRightDist => SensorRightRaw.distance == 0 ? viewDistance : SensorRightRaw.distance;
    protected float SensorLeftDist => SensorLeftRaw.distance == 0 ? viewDistance : SensorLeftRaw.distance;
    protected float SensorBottomDist => SensorBottomRaw.distance == 0 ? viewDistance : SensorBottomRaw.distance;

    protected Coroutine AntiBullsEyeCoroutine { get; set; } = null;

    #endregion
    #region _fields (For Properties)
    protected AIState _currentAIState = AIState.Null;
    protected float _spotScale = 0;
    #endregion

    #region Inspector Fields

    [Header("Physical")]
    [Header("------------------------- AI --------------------------"), Space, Space]
    [Tooltip("The max distance, where the AI can spot the enemy (in meters)")]
    [SerializeField] protected float viewDistance = 10;
    [Tooltip("The Field of view (in degrees)")]
    [SerializeField] protected float fieldOfView = 120;
    [Tooltip("What the AI defines as an enemy and attacks")]
    [SerializeField] protected LayerMask enemyLayerMask = 0;
    [Tooltip("What the AI can see (what could potentialy be an obstacle to see the Enemy)")]
    [SerializeField] protected LayerMask canSeeLayerMask = 0;

    [Header("Brain")]
    [Tooltip("The distance to the target, where the AI starts to move closer (in meters)")]
    [SerializeField] protected float minDistanceToEnemy = 4;
    [Tooltip("The distance to the target, where the AI starts to move farther (in meters)")]
    [SerializeField] protected float maxDistanceToEnemy = 7;
    [Tooltip("Time needed to spot the enemy (in seconds)")]
    [SerializeField] protected float timeToSpot = 5;
    [Tooltip("Time needed to forget about the enemy (in seconds)")]
    [SerializeField] protected float timeToCalm = 10;
    [Tooltip("How fast does the AI looks around (in degrees/sec)")]
    [SerializeField] protected float lookAroundSpeed = 180;
    [Tooltip("How far bullets deviate from the target (in degrees)")]
    [SerializeField] protected float antiBullsEye = 13;

    #endregion

    private void Awake() {
        CurrentAIState = AIState.Calm;
    }

    protected new void Update() {
        base.Update();

        CommonStatePreUpdate();

        switch (CurrentAIState) {
            case AIState.Calm:
                CalmStateUpdate();
                break;
            case AIState.Suspicious:
                SuspiciousStateUpdate();
                break;
            case AIState.Aggressive:
                AggressiveStateUpdate();
                break;
            case AIState.LostTarget:
                LostTargetStateUpdate();
                break;
            case AIState.LookAround:
                LookAroundStateUpdate();
                break;
            case AIState.Busy:
                BusyStateUpdate();
                break;
        }
        //Debug.Log("State: " + CurrentAIState);
        //Debug.Log("SpotScale: " + SpotScale);
        //Debug.Log("enemy: " + Enemy);

        CommonStatePostUpdate();
    }

    #region AI-Service

    protected void CheckForEnemy() {
        // if not so far
        var potentialEnemy = Physics2D.OverlapCircle(transform.position, viewDistance, enemyLayerMask)?.gameObject;
        if (potentialEnemy != null) {
            Vector2 enemyLocalPos = potentialEnemy.transform.position - transform.position;
            // if in the field of view
            if (Mathf.Abs(Mathf.Atan2(enemyLocalPos.y, enemyLocalPos.x) - Mathf.Atan2(LocalLookPos.y, LocalLookPos.x)) * Mathf.Rad2Deg < fieldOfView / 2
                // if no obstacle is between
                && Physics2D.Raycast(transform.position, enemyLocalPos, viewDistance, canSeeLayerMask).collider?.gameObject == potentialEnemy) {
                Enemy = potentialEnemy;
                return;
            }
        }
    }

    protected bool SeesEnemy() {
        if (Enemy == null)
            return false;
        // if not so far
        if (EnemyLocalPos.magnitude < viewDistance
            // if in the field of view
            && Mathf.Abs(Mathf.Atan2(EnemyLocalPos.y, EnemyLocalPos.x)) < fieldOfView / 2
            // if no obstacle is between
            && Physics2D.Raycast(transform.position, EnemyLocalPos, viewDistance, canSeeLayerMask).collider?.gameObject == Enemy)
            return true;
        Enemy = null;
        return false;
    }

    protected abstract void CheckAIState();

    #endregion

    #region State

    // Calm
    protected virtual void OnCalmStateStart() { }
    protected virtual void CalmStateUpdate() { }
    protected virtual void OnCalmStateEnd() { }

    // Suspicious
    protected virtual void OnSuspiciousStateStart() { }
    protected virtual void SuspiciousStateUpdate() { }
    protected virtual void OnSuspiciousStateEnd() { }

    // Aggressive
    protected virtual void OnAggressiveStateStart() { }
    protected virtual void AggressiveStateUpdate() { }
    protected virtual void OnAggressiveStateEnd() { }

    // LostTarget
    protected virtual void OnLostTargetStateStart() { }
    protected virtual void LostTargetStateUpdate() { }
    protected virtual void OnLostTargetStateEnd() { }

    // LookAround
    protected virtual void OnLookAroundStateStart() { }
    protected virtual void LookAroundStateUpdate() { }
    protected virtual void OnLookAroundStateEnd() { }

    // Busy
    protected virtual void OnBusyStateStart() { }
    protected virtual void BusyStateUpdate() { }
    protected virtual void OnBusyStateEnd() { }

    // Common
    protected void CommonStatePreUpdate() {
        if (Enemy != null)
            EnemyLastSeen = Enemy.transform.position;
        if (!SeesEnemy())
            CheckForEnemy();
        CheckAIState();
    }
    protected virtual void CommonStatePostUpdate() {
        Move(WantedMoveDir);
        WantedMoveDir = Vector2.zero;
    }
    protected virtual void OnStateChanged() { }

    public enum AIState {
        Null,
        Calm,
        Aggressive,
        Suspicious,
        LostTarget,
        LookAround,
        Busy
    }

    #endregion

    protected IEnumerator AntiBullsEye() {
        float lastValue = 0;
        while (true) {
            float neededValue = antiBullsEye * Random.Range(-0.5f, 0.5f);
            float delta = neededValue - lastValue;
            float value = lastValue;
            for (int i = 0; i < 10; i++, value += delta / 10f) {
                Inventory.Aim((Quaternion.Euler(0, 0, lastValue = value)) * LocalLookPos, Inventory.CoordsType.Local);
                yield return null;
            }
        }
    }

    protected new void OnDrawGizmos() {
        base.OnDrawGizmos();
        Color moodColor = new Color(1, 1 - SpotScale, 1 - SpotScale);

        // LookPos
        Gizmos.color = moodColor;
        if (Enemy != null) {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + LocalLookPos.normalized * Mathf.Min(minDistanceToEnemy, viewDistance));
            Gizmos.DrawLine((Vector2)transform.position + LocalLookPos.normalized * Mathf.Min(maxDistanceToEnemy, viewDistance), (Vector2)transform.position + LocalLookPos.normalized * viewDistance);
        } else {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + LocalLookPos.normalized * viewDistance);
        }

        // LastSeenEnemyPos
        if (Enemy == null) {
            //Gizmos.DrawSphere(EnemyLastSeen, 0.5f);
            Gizmos.DrawWireCube(EnemyLastSeen, new Vector3(1.2f, 1.8f, 0));
        }

        // View obstacle
        Gizmos.DrawSphere(Physics2D.Raycast(transform.position, LocalLookPos, viewDistance, canSeeLayerMask).point, 0.3f);

        // Sensors
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * SensorRightDist);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * SensorLeftDist);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * SensorBottomDist);

        // AntiBullsEye
        Gizmos.color = Color.yellow;
        for (int sign = -1; sign <= 1; sign += 2)
            Gizmos.DrawLine(transform.position, transform.position +
                Quaternion.Euler(0, 0, sign * antiBullsEye / 2) * LocalLookPos.normalized * viewDistance);
        // FieldOfView
        Gizmos.color = moodColor;
        for (int sign = -1; sign <= 1; sign += 2)
            Gizmos.DrawLine(transform.position, transform.position +
                Quaternion.Euler(0, 0, sign * fieldOfView / 2) * LocalLookPos.normalized * viewDistance);
        // viewDistance
        for (float i = 0; i < 1f;) {
            Gizmos.DrawLine(
                transform.position + Quaternion.Euler(0, 0, fieldOfView * (0.5f - i)) * LocalLookPos.normalized * viewDistance,
                transform.position + Quaternion.Euler(0, 0, fieldOfView * (0.5f - (i += 1f / 10f))) * LocalLookPos.normalized * viewDistance);
        } //                                                                                  ↑ accuracy
    }
}
