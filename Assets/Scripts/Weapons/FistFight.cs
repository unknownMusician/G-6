using G6.Characters;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace G6.Weapons {
    public class FistFight : MonoBehaviour {

        const string TAG = "FistFight: ";

        #region Properties

        protected bool CanAttack {
            get => canAttack;
            set { if (!(canAttack = value)) SetReliefTimer(1 / attackSpeedMultiplier); }
        }
        private Vector3 WorldFistFightCentrePoint => transform.position + this.transform.rotation * localFistFightCentrePoint;

        #endregion

        #region Variables

        [SerializeField]
        private Vector3 localFistFightCentrePoint = Vector3.right;
        [SerializeField]
        private float fistFightAreaRadius = 1f;

        [Space]
        [SerializeField]
        float attackSpeedMultiplier = 1f;
        [SerializeField]
        float damage = 1f;

        //////////

        protected Timer timer;
        protected bool canAttack = true;

        #endregion

        #region Gizmos

        private void OnDrawGizmos() {
            Gizmos.color = new Color(1f, 0.2f, 0f);
            Gizmos.DrawWireSphere(WorldFistFightCentrePoint, fistFightAreaRadius);
        }

        #endregion

        #region Service Methods

        protected void SetReliefTimer(float time) {
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(time * 1000);
            // Hook up the Elapsed event for the timer.
            timer.Elapsed += (sender, e) => { CanAttack = true; };
            timer.AutoReset = false;
            timer.Enabled = true;
        }

        #endregion

        #region Main Methods

        public void Attack() {
            if (CanAttack) {
                Collider2D[] cols = Physics2D.OverlapCircleAll(WorldFistFightCentrePoint, fistFightAreaRadius);
                //
                int actualHits = (from col in cols
                                  group col by col.gameObject into gameObj
                                  where !gameObj.Key.Equals(this.transform.parent.gameObject)
                                  group gameObj.Key by gameObj.Key.GetComponent<CharacterBase>() into charBase
                                  where charBase.Key != null
                                  let hitPoint = charBase.Key.gameObject.transform.position
                                  select charBase.Key)
                            .Select(x => {
                                x.TakeDamage((x.gameObject.transform.position - transform.position).normalized * damage);
                                return x;
                            })
                            .Count();
                CanAttack = false;
                Debug.Log(TAG + "Hit (" + actualHits + " target" + ((actualHits == 1) ? "" : "s") + ")");
            }
        }

        #endregion
    }
}