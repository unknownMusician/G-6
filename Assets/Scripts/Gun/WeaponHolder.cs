using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {
    [SerializeField]
    private GameObject child;
    public GameObject Child { get; set; }

    void Update() {
        if (Child == null && this.transform.childCount > 0) {
            Child = this.transform.GetChild(0).gameObject;
        }
        if (Child != null) {
            Child.transform.localPosition = new Vector2(0, Mathf.Sin(Time.time*2)/4 + 0.5f);
        }
    }
    public void Remove() {
        Destroy(this.gameObject);
    }
    public void SetChild(Transform ch) {
        this.Child = ch.gameObject;
        ch.SetParent(this.transform);
    }
}
