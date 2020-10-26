using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehaviour : AIBase {

    [SerializeField] protected float speakDistance;
    [SerializeField] protected float talkTime;

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

    protected override void CalmStateUpdate() {
        if (CoroutineDoNotTalk == null && CoroutineTalkToOther == null) {
            // searching for a partner to speak
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, speakDistance, 1 << gameObject.layer);
            if (cols != null) {
                List<AIBehaviour> partners = (from col in cols
                                              group col by col.gameObject into gameObj
                                              where !gameObj.Key.Equals(gameObject)
                                              group gameObj.Key by gameObj.Key.GetComponent<AIBehaviour>() into aib
                                              where aib.Key != null
                                              select aib.Key).ToList();
                // ask to speak if found
                if (partners.Count > 0 && partners[0].InteractWithAI(this)) {
                    // can't see enemy
                    WorldLookPos = partners[0].gameObject.transform.position;
                    // stops walking and start speaking
                    StopCalmCoroutines();
                    CoroutineTalkToOther = StartCoroutine(TalkToOther(talkTime));
                }
            }
        }
    }

    protected override void OnCalmStateEnd() {
        IsCrouching = false;
        StopCalmCoroutines();
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
        Inventory.AttackStart();
    }

    protected override void OnAggressiveStateEnd() {
        // DON'T Attack the Enemy
        Inventory.AttackEnd();

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
    #region Busy

    protected override void OnBusyStateStart() {

    }

    protected override void OnBusyStateEnd() {

    }

    #endregion

    #region Common

    protected override void CommonStatePostUpdate() {
        if (BulletsClip == 0)
            Inventory.ReloadGun();

        if (CurrentAIState != AIState.Aggressive)
            Inventory.Aim(LocalLookPos, Inventory.CoordsType.Local);

        // Jumping over obstacles
        bool needToJumpUp;
        if ((needToJumpUp = WantedMoveDir.y > 0) != (State == State.Climb)) {
            if (needToJumpUp) {
                if ((WantedMoveDir.x > 0 && SensorRightDist < 1) || (WantedMoveDir.x < 0 && SensorLeftDist < 1))
                    Jump();
            } else {
                if ((WantedMoveDir.x < 0 && SensorRightDist < 1) || (WantedMoveDir.x > 0 && SensorLeftDist < 1))
                    Jump();
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
        howLongGo += Random.Range(-0.5f, 0.5f);
        howLongStand += Random.Range(-0.5f, 0.5f);
        bool goLeftFirst = Random.value > 0.5f;
        while (true) {
            if (!goLeftFirst) {
                for (float i = 0; i < howLongGo; i += Time.deltaTime) {
                    // Go right
                    LocalLookPos = Vector2.right;
                    WantedMoveDir = new Vector2(1, 1);
                    yield return null;
                }
                for (float i = 0; i < howLongStand; i += Time.deltaTime) {
                    // Stop
                    yield return null;
                }
            }
            for (float i = 0; i < howLongGo; i += Time.deltaTime) {
                // Go left
                LocalLookPos = Vector2.left;
                WantedMoveDir = new Vector2(-1, 1);
                yield return null;
            }
            for (float i = 0; i < howLongStand; i += Time.deltaTime) {
                // Stop
                yield return null;
            }
            if (goLeftFirst) {
                for (float i = 0; i < howLongGo; i += Time.deltaTime) {
                    // Go right
                    LocalLookPos = Vector2.right;
                    WantedMoveDir = new Vector2(1, 1);
                    yield return null;
                }
                for (float i = 0; i < howLongStand; i += Time.deltaTime) {
                    // Stop
                    yield return null;
                }
            }
        }
    }
    protected Coroutine CoroutineTalkToOther { get; set; }
    protected IEnumerator TalkToOther(float timeToTalk) {
        float timeLeft = timeToTalk;
        string[] phrases = { "Hi", "You ok?", "I'm ok", "Well...", "Uh...", "Nice weather, by the way", "Yeah, uhm...", "Ok, bye..." };
        float delay = Random.Range(0, 0.8f);
        yield return new WaitForSeconds(delay);
        timeLeft -= delay;
        while (timeLeft > 0) {
            Debug.Log($"{name}: {phrases[Random.Range(0, phrases.Length - 1)]}");
            yield return new WaitForSeconds(Mathf.Min(3, timeLeft));
            timeLeft -= 3;
        }
        Debug.Log($"{name}: {phrases[phrases.Length - 1]}");
        CoroutineTalkToOther = null;
        CoroutineDoNotTalk = StartCoroutine(DoNotTalk(10));
        StartRandomCalmRoutine();
    }
    protected Coroutine CoroutineDoNotTalk { get; set; }
    protected IEnumerator DoNotTalk(float time) {
        yield return new WaitForSeconds(time);
        CoroutineDoNotTalk = null;
    }
    protected Coroutine CoroutineWhistle { get; set; }
    protected IEnumerator Whistle(float time) {
        float timeLeft = time;
        string[] whistles = { "♩", "♪", "♫", "♬", "♭♩", "♯♫", "♩.♪", "♫ ♪" };
        float delay = Random.Range(0, 0.8f);
        yield return new WaitForSeconds(delay);
        timeLeft -= delay;
        while (timeLeft > 0) {
            LocalLookPos = Vector2.up;
            Debug.Log($"{name}: {whistles[Random.Range(0, whistles.Length)]}");
            yield return new WaitForSeconds(Mathf.Min(3, timeLeft));
            timeLeft -= 3;
        }
        CoroutineWhistle = null;
        StartRandomCalmRoutine();
    }
    protected Coroutine CoroutineSleep { get; set; }
    protected IEnumerator Sleep(float time) {
        float timeLeft = time;
        string[] whistles = { "z...", "Zz...", "Zzz...", "Zzzz..." };
        float delay = Random.Range(0, 0.8f);
        yield return new WaitForSeconds(delay);
        timeLeft -= delay;
        while (timeLeft > 0) {
            LocalLookPos = Vector2.down;
            Debug.Log($"{name}: {whistles[Random.Range(0, whistles.Length)]}");
            yield return new WaitForSeconds(Mathf.Min(3, timeLeft));
            timeLeft -= 3;
        }
        CoroutineSleep = null;
        StartRandomCalmRoutine();
    }

    protected void StartRandomCalmRoutine() {
        int rd = Random.Range(0, 2);
        if (rd == 0)
            CoroutineWalkAround = StartCoroutine(WalkAround(2, 2));
        else if (rd == 1)
            CoroutineWhistle = StartCoroutine(Whistle(10));
        else if (rd == 2)
            CoroutineSleep = StartCoroutine(Sleep(10));
    }
    protected void StopCalmCoroutines() {
        if (CoroutineWalkAround != null) {
            StopCoroutine(CoroutineWalkAround);
            CoroutineWalkAround = null;
        }
        if (CoroutineTalkToOther != null) {
            StopCoroutine(CoroutineTalkToOther);
            CoroutineTalkToOther = null;
        }
        if (CoroutineWhistle != null) {
            StopCoroutine(CoroutineWhistle);
            CoroutineWhistle = null;
        }
        if (CoroutineSleep != null) {
            StopCoroutine(CoroutineSleep);
            CoroutineSleep = null;
        }
    }
    #endregion

    #region Interaction
    public bool InteractWithAI(AIBehaviour otherAI) {
        if (CurrentAIState != AIState.Calm || CoroutineTalkToOther != null || CoroutineDoNotTalk != null)
            return false;
        // can't see enemy
        WorldLookPos = otherAI.gameObject.transform.position;
        // stops walking and start speaking
        StopCalmCoroutines();
        CoroutineWalkAround = null;
        CoroutineTalkToOther = StartCoroutine(TalkToOther(talkTime));
        return true;
    }
    #endregion
}
