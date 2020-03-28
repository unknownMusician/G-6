using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> weapons;

    private void Start() {
        for(int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            Weapon weapon = child.GetComponent<Weapon>();
            if(weapon != null) {
                for(int j = 0; j < weapons.Count; j++) {
                    if(weapons[j] == null) {
                        weapons[j] = weapon;
                        break;
                    }
                }
            }
        }
    }
}
