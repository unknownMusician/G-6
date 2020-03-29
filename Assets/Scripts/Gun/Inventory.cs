using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> weapons;
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
        weapons[activeWeapon].Attack();
    }

    public void ChangeState() {
        weapons[activeWeapon].ChangeState();
    }

    public void Choose(int index) {
        if(index < 0 || index >= this.transform.childCount) {
            Debug.Log("This is an IndexOutOfBoundsExeption in Inventory");
            return;
        }
        if(index == activeWeapon) {
            return;
        }
        weapons[activeWeapon].gameObject.SetActive(false);
        weapons[index].gameObject.SetActive(true);
        activeWeapon = index;
    }

    public void ChooseNext() {
        if(activeWeapon == weapons.Count - 1) {
            weapons[activeWeapon].gameObject.SetActive(false);
            weapons[0].gameObject.SetActive(true);
            activeWeapon = 0;
            return;
        }
        weapons[activeWeapon].gameObject.SetActive(false);
        weapons[activeWeapon + 1].gameObject.SetActive(true);
        activeWeapon++;
    }

    public void ChoosePrev() {
        if (activeWeapon == 0) {
            weapons[activeWeapon].gameObject.SetActive(false);
            weapons[weapons.Count - 1].gameObject.SetActive(true);
            activeWeapon = weapons.Count - 1;
            return;
        }
        weapons[activeWeapon].gameObject.SetActive(false);
        weapons[activeWeapon - 1].gameObject.SetActive(true);
        activeWeapon--;
    }

    public int GetCount() {
        return weapons.Count;
    }
}
