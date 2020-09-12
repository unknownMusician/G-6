using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIBehaviour : CharacterBase {

    #region Properties

    private AIState CurrentAIState {
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
            }
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
            }
        }
    }

    private GameObject Enemy { get => _enemy; set => _enemy = value; }

    private Vector2 LocalLookPos {
        get => _localLookPos;
        set {
            _localLookPos = value;
            //Inventory.Aim(_localLookPos, Inventory.CoordsType.Local);
        }
    }
    private Vector2 WorldLookPos => (Vector2)transform.position + LocalLookPos;
    private Vector2 EnemyPos => Enemy.transform.position;
    private Vector2 EnemyLocalPos => EnemyPos - (Vector2)transform.position;
    private Vector2 EnemyLastSeen { get; set; } = Vector2.zero;

    private float DistanceToEnemy => EnemyLocalPos.magnitude;
    private float SpotScale { get => _spotScale; set => _spotScale = value > 1 ? 1 : (value < 0 ? 0 : value); }

    private int BulletsClip => (Inventory.Weapon is Gun gun) ? gun.ActualClipBullets : 0;
    private int BulletsPocket => (Inventory.Weapon is Gun gun) ? gun.ActualPocketBullets : 0;

    private Coroutine AntiBullsEyeCoroutine { get; set; } = null;

    #endregion
    #region _fields (For Properties)
    private AIState _currentAIState = AIState.Calm;
    private float _spotScale = 0;
    private GameObject _enemy = null;
    private Vector2 _localLookPos = Vector2.right;
    #endregion

    #region Inspector Fields

    [Space, Space, Space]

    [SerializeField] private float viewDistance = 5;
    [SerializeField] private float fieldOfView = 50; // (in degrees)
    [SerializeField] private LayerMask enemyLayerMask = 0;
    [SerializeField] private LayerMask canSeeLayerMask = 0;
    [Space]
    [SerializeField] private float minDistanceToEnemy = 2;
    [SerializeField] private float maxDistanceToEnemy = 4;
    [SerializeField] private float timeToSpot = 5; // (in seconds)
    [SerializeField] private float timeToCalm = 5; // (in seconds)
    [SerializeField] private float lookAroundSpeed = 5; // (in Geg/Sec)
    [SerializeField] private float antiBullsEye = 5; // (in degrees)

    #endregion

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

    /* Scheme
         __Calm__
            ↓
        Suspicious ← ← ← \
        ↓     ↓          ↑
        ↓   Aggressive   ↑
        ↓      ↓         ↑
        LostTarget → → → ↑
        ↓      ↓         ↑
        ↓   LookAround → /
        ↓      ↓
        __Calm__
     */
    private void CheckAIState() {
        switch (CurrentAIState) {
            case AIState.Calm:
                if (Enemy != null)
                    CurrentAIState = AIState.Suspicious;
                break;
            case AIState.Suspicious:
                if (Enemy == null)
                    CurrentAIState = AIState.LostTarget;
                else if (SpotScale == 1)
                    CurrentAIState = AIState.Aggressive;
                break;
            case AIState.Aggressive:
                if (Enemy == null)
                    CurrentAIState = AIState.LostTarget;
                break;
            case AIState.LostTarget:
                if (Enemy != null)
                    CurrentAIState = AIState.Suspicious;
                else if ((Mathf.Abs(EnemyLastSeen.x - transform.position.x)) <= 1)
                    CurrentAIState = AIState.LookAround;
                else if (SpotScale == 0)
                    CurrentAIState = AIState.Calm;
                break;
            case AIState.LookAround:
                if (Enemy != null)
                    CurrentAIState = AIState.Suspicious;
                else if (SpotScale == 0)
                    CurrentAIState = AIState.Calm;
                break;
        }
    }

    #endregion

    #region State

    #region Calm

    protected void OnCalmStateStart() { /* todo */ }

    protected void CalmStateUpdate() {
        LocalLookPos = Vector2.right;
        // stand & walk
        Move(Vector2.zero);
    }

    protected void OnCalmStateEnd() { /* todo */ }

    #endregion
    #region Suspicious

    protected void OnSuspiciousStateStart() { /* todo */ }

    protected void SuspiciousStateUpdate() {
        // Higher the spot scale with time
        SpotScale += (1 / timeToSpot) * viewDistance / DistanceToEnemy * Time.deltaTime;

        // Look at the Enemy
        LocalLookPos = EnemyLocalPos;

        // Move slowly to the enemy
        IsSneaking = true;
        Move(EnemyLocalPos);
    }

    protected void OnSuspiciousStateEnd() {
        // Stop moving slowly to the enemy
        IsSneaking = false;
        Move(Vector2.zero);
    }

    #endregion
    #region Aggressive

    protected void OnAggressiveStateStart() {
        // start "shoot-spreading"
        AntiBullsEyeCoroutine = StartCoroutine(AntiBullsEye());
    }

    protected void AggressiveStateUpdate() {
        // Look at the Enemy
        LocalLookPos = EnemyLocalPos;

        // Move to or from the enemy
        if (DistanceToEnemy > maxDistanceToEnemy)
            Move(EnemyLocalPos);
        else if (DistanceToEnemy < minDistanceToEnemy)
            Move(-EnemyLocalPos);
        else
            Move(Vector2.zero);

        // Attack the Enemy
        Inventory.AttackWithWeaponOrFistStart();
    }

    protected void OnAggressiveStateEnd() {
        // DON'T Attack the Enemy
        Inventory.AttackWithWeaponOrFistEnd();

        Inventory.ReloadGun();
        // stop "shoot-spreading"
        StopCoroutine(AntiBullsEyeCoroutine);
    }

    #endregion
    #region LostTarget

    protected void OnLostTargetStateStart() { /* todo */ }

    protected void LostTargetStateUpdate() {
        // Look at the position, where an enemy can be
        LocalLookPos = EnemyLastSeen - (Vector2)transform.position;

        // Moving to the position, where an enemy can be
        Move(LocalLookPos);

        // Lower the spot scale with time
        SpotScale -= (1 / timeToCalm) * Time.deltaTime;
    }

    protected void OnLostTargetStateEnd() {
        // Stop moving to the position, where an enemy can be
        Move(Vector2.zero);
    }

    #endregion
    #region LookAround

    protected void OnLookAroundStateStart() { /* todo */ }

    protected void LookAroundStateUpdate() {
        // searching for the enemy
        LocalLookPos = Quaternion.Euler(0, 0, lookAroundSpeed * Time.deltaTime) * LocalLookPos;

        // Lower the spot scale with time
        SpotScale -= (1 / timeToCalm) * Time.deltaTime;
    }

    protected void OnLookAroundStateEnd() {
        // todo
    }

    #endregion

    #region Common

    protected void CommonStatePreUpdate() {
        if (Enemy != null)
            EnemyLastSeen = Enemy.transform.position;
        if (!SeesEnemy())
            CheckForEnemy();
        CheckAIState();
    }

    protected void CommonStatePostUpdate() {
        if (BulletsClip == 0)
            Inventory.ReloadGun();
    }

    #endregion

    protected enum AIState {
        Calm,
        Aggressive,
        Suspicious,
        LostTarget,
        LookAround
    }

    #endregion

    IEnumerator AntiBullsEye() {
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

    private void OnDrawGizmos() {
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
            Gizmos.color = Color.red;
            //Gizmos.DrawSphere(EnemyLastSeen, 0.5f);
            Gizmos.DrawWireCube(EnemyLastSeen, new Vector3(1.2f, 1.8f, 0));
        }

        Gizmos.color = moodColor;
        // View obstacle
        Gizmos.DrawSphere(Physics2D.Raycast(transform.position, LocalLookPos, viewDistance, canSeeLayerMask).point, 0.3f);

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
