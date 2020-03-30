using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> weapons = null;
    private int activeWeapon; // index

    private void Start() {
        Prepare();
    }

    private void Prepare() {
        for (int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            Weapon weapon = child.GetComponent<Weapon>();
            if (weapon != null) {
                for (int j = 0; j < weapons.Count; j++) {
                    if (weapons[j] == null) {
                        weapons[j] = weapon;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < this.transform.childCount; i++) {
            if (i == 0) {
                weapons[0].gameObject.SetActive(true);
                activeWeapon = 0;
                continue;
            }
            weapons[i].gameObject.SetActive(false);
        }
    }
    public void Attack() {
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].Attack();
        }
    }
    public void Throw() {
        if(weapons[activeWeapon] != null) {
            Weapon wp = weapons[activeWeapon];
            Rigidbody2D wpRb = wp.gameObject.GetComponent<Rigidbody2D>();

            wpRb.bodyType = RigidbodyType2D.Dynamic; // "enabled" Rigidbody2D

            wp.GetWeaponCollider().enabled = true; // enabled Collider2D

            Vector2 force = this.gameObject.transform.rotation * Vector2.right;
            wpRb.velocity += force; // "throwed" the weapon

            wp.gameObject.transform.parent = null; // unparented weapon

            weapons[activeWeapon] = null;
        }
    }
    public void ChangeState() {
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].ChangeState();
        }
    }
    public void Choose(int index) {
        if(index < 0 || index >= this.transform.childCount) {
            Debug.Log("This is an IndexOutOfBoundsExeption in Inventory");
            return;
        }
        if(index == activeWeapon) {
            return;
        }
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].gameObject.SetActive(false);
        }
        if (weapons[index] != null) {
            weapons[index].gameObject.SetActive(true);
        }
        activeWeapon = index;
    }
    public void ChooseNext() {
        if(activeWeapon == weapons.Count - 1) {
            if (weapons[activeWeapon] != null) {
                weapons[activeWeapon].gameObject.SetActive(false);
            }
            if (weapons[0] != null) {
                weapons[0].gameObject.SetActive(true);
            }
            activeWeapon = 0;
            return;
        }
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].gameObject.SetActive(false);
        }
        if (weapons[activeWeapon + 1] != null) {
            weapons[activeWeapon + 1].gameObject.SetActive(true);
        }
        activeWeapon++;
    }
    public void ChoosePrev() {
        if (activeWeapon == 0) {
            if (weapons[activeWeapon] != null) {
                weapons[activeWeapon].gameObject.SetActive(false);
            }
            if (weapons[weapons.Count - 1] != null) {
                weapons[weapons.Count - 1].gameObject.SetActive(true);
            }
            activeWeapon = weapons.Count - 1;
            return;
        }
            if (weapons[activeWeapon] != null) {
                weapons[activeWeapon].gameObject.SetActive(false);
            }
        if (weapons[activeWeapon - 1] != null) {
            weapons[activeWeapon - 1].gameObject.SetActive(true);
        }
        activeWeapon--;
    }
    public int GetCount() {
        return weapons.Count;
    }
}
