using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateHolder : MonoBehaviour {
    public GameObject Child => transform.childCount == 0 ? null : transform.GetChild(0)?.gameObject;

    void Update() {
        if (Child != null)
            Child.transform.localPosition = new Vector2(0, Mathf.Sin(Time.time * 2) / 4 + 0.5f);
        else
            Destroy(gameObject);
    }
}
