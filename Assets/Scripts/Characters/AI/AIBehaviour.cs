using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : AIBase {
    #region State Order
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
    protected override void CheckAIState() {
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
                else if ((EnemyLastSeen - (Vector2)transform.position).magnitude <= 1)
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

    protected override void OnCalmStateStart() {
        EnemyLastSeen = Vector2.zero;
        IsCrouching = true;
        CoroutineWalkAround = StartCoroutine(WalkAround(2, 2));
    }

    protected override void OnCalmStateEnd() {
        IsCrouching = false;
        StopCoroutine(CoroutineWalkAround);
    }

    #endregion
    #region Suspicious

    protected override void SuspiciousStateUpdate() {
        // Higher the spot scale with time
        SpotScale += (1 / timeToSpot) * viewDistance / DistanceToEnemy * Time.deltaTime;

        // Look at the Enemy
        LocalLookPos = EnemyLocalPos;

        // Move slowly to the enemy
        IsCrouching = true;
        WantedMoveDir = EnemyLocalPos;
    }

    protected override void OnSuspiciousStateEnd() {
        // Stop moving slowly to the enemy
        IsCrouching = false;
    }

    #endregion
    #region Aggressive

    protected override void OnAggressiveStateStart() {
        // start "shoot-spreading"
        AntiBullsEyeCoroutine = StartCoroutine(AntiBullsEye());
    }

    protected override void AggressiveStateUpdate() {
        // Look at the Enemy
        LocalLookPos = EnemyLocalPos;

        // Move to or from the enemy
        if (DistanceToEnemy > maxDistanceToEnemy)
            WantedMoveDir = EnemyLocalPos;
        else if (DistanceToEnemy < minDistanceToEnemy)
            WantedMoveDir = -EnemyLocalPos;

        // Attack the Enemy
        Inventory.AttackWithWeaponOrFistStart();
    }

    protected override void OnAggressiveStateEnd() {
        // DON'T Attack the Enemy
        Inventory.AttackWithWeaponOrFistEnd();

        Inventory.ReloadGun();
        // stop "shoot-spreading"
        StopCoroutine(AntiBullsEyeCoroutine);
    }

    #endregion
    #region LostTarget

    protected override void LostTargetStateUpdate() {
        // Lower the spot scale with time
        SpotScale -= (1 / timeToCalm) * Time.deltaTime;

        // Look at the position, where an enemy can be
        LocalLookPos = EnemyLastSeen - (Vector2)transform.position;

        // Moving to the position, where an enemy can be
        WantedMoveDir = LocalLookPos;
    }

    protected override void OnLostTargetStateEnd() {

    }

    #endregion
    #region LookAround

    protected override void LookAroundStateUpdate() {
        // Lower the spot scale with time
        SpotScale -= (1 / timeToCalm) * Time.deltaTime;

        // searching for the enemy
        LocalLookPos = Quaternion.Euler(0, 0, lookAroundSpeed * Time.deltaTime) * LocalLookPos;
    }

    #endregion

    #region Common

    protected override void CommonStatePostUpdate() {
        if (BulletsClip == 0)
            Inventory.ReloadGun();

        if (CurrentAIState != AIState.Aggressive)
            Inventory.Aim(LocalLookPos, Inventory.CoordsType.Local);

        Debug.Log("Can Jump");
        // Jumping over obstacles
        bool needToJumpUp;
        if ((needToJumpUp = WantedMoveDir.y > 0) != (State == State.Climb)) {
            if (needToJumpUp) {
                if ((WantedMoveDir.x > 0 && SensorRightDist < 1) || (WantedMoveDir.x < 0 && SensorLeftDist < 1)) {
                    Jump();
                    Debug.Log("Jump");
                }
            } else {
                if ((WantedMoveDir.x < 0 && SensorRightDist < 1) || (WantedMoveDir.x > 0 && SensorLeftDist < 1)) {
                    Jump();
                    Debug.Log("Jump");
                }
            }
        }

        // Rememder LastVelocity
        LastPosition = transform.position;

        base.CommonStatePostUpdate();
    }

    #endregion

    #endregion

    #region Coroutines
    protected Coroutine CoroutineWalkAround { get; set; }
    protected IEnumerator WalkAround(float howLongGo, float howLongStand) {
        while (true) {
            for (float i = 0; i < howLongGo; i += Time.deltaTime) {
                // Go right
                LocalLookPos = Vector2.right;
                WantedMoveDir = Vector2.right;
                yield return null;
            }
            for (float i = 0; i < howLongStand; i += Time.deltaTime) {
                // Stop
                yield return null;
            }
            for (float i = 0; i < howLongGo; i += Time.deltaTime) {
                // Go left
                LocalLookPos = Vector2.left;
                WantedMoveDir = Vector2.left;
                yield return null;
            }
            for (float i = 0; i < howLongStand; i += Time.deltaTime) {
                // Go right
                yield return null;
            }
        }
    }
    #endregion

    public override void Interact(GameObject whoInteracted) { /* todo */ }
}
