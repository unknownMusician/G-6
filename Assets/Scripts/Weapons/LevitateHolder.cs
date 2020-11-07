using UnityEngine;

namespace G6.Weapons {
    public class LevitateHolder : MonoBehaviour {
        public GameObject Child => transform.childCount == 0 ? null : transform.GetChild(0)?.gameObject;

        void Update() {
            if (Child != null)
                Child.transform.localPosition = new Vector2(0, Mathf.Sin(Time.time * 2) / 4 + 0.5f);
            else
                Destroy(gameObject);
        }
    }
}