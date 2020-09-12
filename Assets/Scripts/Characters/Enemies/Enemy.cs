using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : CharacterBase {

    #region Properties

    private Vector2 LocalLookPos { get; set; } = Vector2.right;
    private Vector2 WorldLookPos => (Vector2)transform.position + LocalLookPos;
    private AIState CurrentAIState {
        get => _currentAIState;
        set {
            var lastState = _currentAIState;
            if (_currentAIState == value)
                return;
            _currentAIState = value;

            switch (lastState) {
                case AIState.Aggressive:
                    OnAggressiveStateEnd();
                    break;
                case AIState.Calm:
                    OnCalmStateEnd();
                    break;
                case AIState.Wondering:
                    OnWonderingStateEnd();
                    break;
            }
        }
    }

    private Vector2 EnemyPos => (Vector2)enemy?.transform.position;
    private Vector2 EnemyLocalPos => EnemyPos - (Vector2)transform.position;
    private float DistanceToEnemy => EnemyLocalPos.magnitude;

    private int BulletsClip => (Inventory.Weapon is Gun gun) ? gun.ActualClipBullets : 0;
    private int BulletsPocket => (Inventory.Weapon is Gun gun) ? gun.ActualPocketBullets : 0;

    #endregion
    #region _fields (For Properties)
    private AIState _currentAIState = AIState.Calm;
    #endregion

    #region Fields

    #region Inspector Values

    [Space, Space, Space]

    [SerializeField] private float viewDistance = 5;
    [SerializeField] private float fieldOfView = 50;
    [SerializeField] private LayerMask enemyLayerMask = 0;
    [SerializeField] private LayerMask canSeeLayerMask = 0;
    [Space]
    [SerializeField] private float minDistanceToEnemy = 2;
    [SerializeField] private float maxDistanceToEnemy = 4;

    #endregion

    #region Private Values

    private GameObject enemy = null;
    private Vector2 enemyLastSeen = Vector2.zero;

    #endregion

    #endregion

    #region Mono

    protected new void Update() {
        base.Update();

        CommonStatePreUpdate();

        switch (CurrentAIState) {
            case AIState.Calm:
                CalmStateUpdate();
                break;
            case AIState.Aggressive:
                AggressiveStateUpdate();
                break;
            case AIState.Wondering:
                WonderingStateUpdate();
                break;
        }
        //Debug.Log("State: " + CurrentAIState);
        //Debug.Log("enemy: " + enemy);

        CommonStatePostUpdate();
    }

    #endregion

    #region AI

    protected void FindEnemy() {
        // if not so far
        var potentialEnemy = Physics2D.OverlapCircle(transform.position, viewDistance, enemyLayerMask)?.gameObject;
        if (potentialEnemy != null) {
            Vector2 enemyLocalPos = potentialEnemy.transform.position - transform.position;
            // if in the field of view
            if (Mathf.Abs(Mathf.Atan2(enemyLocalPos.y, enemyLocalPos.x) - Mathf.Atan2(LocalLookPos.y, LocalLookPos.x)) * Mathf.Rad2Deg < fieldOfView / 2
                // if no obstacle is between
                && Physics2D.Raycast(transform.position, enemyLocalPos, viewDistance, canSeeLayerMask).collider?.gameObject == potentialEnemy) {
                enemy = potentialEnemy;
                CurrentAIState = AIState.Aggressive;
                return;
            }
        }
    }

    protected bool SeesEnemy() {
        if (enemy == null)
            return false;
        // if not so far
        if (EnemyLocalPos.magnitude < viewDistance
            // if in the field of view
            && Mathf.Abs(Mathf.Atan2(EnemyLocalPos.y, EnemyLocalPos.x)) < fieldOfView / 2
            // if no obstacle is between
            && Physics2D.Raycast(transform.position, EnemyLocalPos, viewDistance, canSeeLayerMask).collider?.gameObject == enemy)
            return true;
        enemy = null;
        CurrentAIState = AIState.Calm;
        return false;
    }

    #endregion

    #region State

    #region Calm

    protected void CalmStateUpdate() {
        // stand & walk
        Move(Vector2.zero);
    }

    protected void OnCalmStateEnd() {
        // todo
    }

    #endregion
    #region Aggressive

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

        // Aim at the Enemy
        Inventory.Aim(EnemyPos, Inventory.CoordsType.World);

        // Attack the Enemy
        Inventory.AttackWithWeaponOrFistStart();

        // Remember Enemy position
        enemyLastSeen = enemy.transform.position;
    }

    protected void OnAggressiveStateEnd() {
        // DON'T Attack the Enemy
        Inventory.AttackWithWeaponOrFistEnd();

        Inventory.ReloadGun();
    }

    #endregion
    #region Wondering

    protected void WonderingStateUpdate() {
        // searching for the enemy
        if ((enemyLastSeen - (Vector2)transform.position).magnitude > 1)
            Move(enemyLastSeen);
    }

    protected void OnWonderingStateEnd() {

    }

    #endregion

    #region Common

    protected void CommonStatePreUpdate() {

        if (!SeesEnemy())
            FindEnemy();
    }

    protected void CommonStatePostUpdate() {
        if (BulletsClip == 0)
            Inventory.ReloadGun();
    }

    #endregion

    /////////////////////////

    #region Enum (AIState)

    protected enum AIState {
        Calm,
        Aggressive,
        Wondering
    }

    #endregion

    #endregion

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.gray;
        // viewDistance
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        // LookPos
        if (enemy != null) {
            Gizmos.color = Color.red;
            Vector2 minD;
            Gizmos.DrawLine(transform.position, minD = (Vector2)transform.position + LocalLookPos.normalized * Mathf.Min(minDistanceToEnemy, viewDistance));
            Gizmos.color = Color.green;
            Vector2 maxD;
            Gizmos.DrawLine(minD, maxD = (Vector2)transform.position + LocalLookPos.normalized * Mathf.Min(maxDistanceToEnemy, viewDistance));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(maxD, (Vector2)transform.position + LocalLookPos.normalized * viewDistance);
        } else {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + LocalLookPos.normalized * viewDistance);
        }
        // view obstacle

        Gizmos.DrawSphere(Physics2D.Raycast(transform.position, LocalLookPos, viewDistance, canSeeLayerMask).point, 0.2f);
        // FieldOfView
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, fieldOfView / 2) * LocalLookPos.normalized * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -fieldOfView / 2) * LocalLookPos.normalized * viewDistance);
    }

    #endregion
}
