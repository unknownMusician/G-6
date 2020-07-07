using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    const string TAG = "Inventory: ";

    private static List<Card> cards = new List<Card>();
    public static List<Card> Cards { get; }
    public static bool AddCard(Card card) {
        if (cards.Count >= 10)
            return false;
        cards.Add(card);
        return true;
    }

    public static bool RemoveCard(Card card) {
        return cards.Remove(card);
    }

    #region Actions

    // 

    #endregion

    #region Constants

    public static class Slots {
        readonly public static int FIRST = 0;
        readonly public static int SECOND = 1;
        readonly public static int THIRD = 2;
        readonly public static int FOURTH = 3;
    }

    #endregion

    #region Parameters

    public Weapon Weapon { get { return weapons[activeWeapon]; } }

    #endregion

    #region Public Variables

    [SerializeField]
    private List<Weapon> weapons = null;
    [SerializeField]
    protected float throwStrenght;
    [SerializeField]
    protected float secondsToMaxThrow;

    #endregion

    #region Private Variables

    private int activeWeapon; // index
    protected float tmpWhenThrowButtonPressed;

    #endregion

    #region Overrided Methods

    private void Start() {
        Prepare();
    }

    #endregion

    #region Service Methods

    private void SendToMainData() {
        MainData.ActiveWeapon = Weapon.WeaponPrefab;

        Dictionary<GameObject, List<Card>> allWeapons = new Dictionary<GameObject, List<Card>>();
        for(int i = 0; i < weapons.Count; i++) {
            if (weapons[i] != null) {
                allWeapons.Add(weapons[i].WeaponPrefab, weapons[i].GetAllCardsList());
            }
        }
        MainData.InventoryWeapons = allWeapons;
        Debug.Log(TAG + "SendToMainData()");
    }

    #endregion

    #region WorkingWithSlots Methods

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
    public void Choose(int index) {
        if (index < 0 || index >= this.transform.childCount) {
            Debug.Log(TAG + "This is an IndexOutOfBoundsExeption in Inventory");
            return;
        }
        if (index == activeWeapon) {
            return;
        }
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].gameObject.SetActive(false);
        }
        if (weapons[index] != null) {
            weapons[index].gameObject.SetActive(true);
        }
        activeWeapon = index;
        SendToMainData();
    }
    public void ChooseNext() {
        if (activeWeapon == weapons.Count - 1) {
            if (weapons[activeWeapon] != null) {
                weapons[activeWeapon].gameObject.SetActive(false);
            }
            if (weapons[0] != null) {
                weapons[0].gameObject.SetActive(true);
            }
            activeWeapon = 0;
            SendToMainData();
            return;
        }
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].gameObject.SetActive(false);
        }
        if (weapons[activeWeapon + 1] != null) {
            weapons[activeWeapon + 1].gameObject.SetActive(true);
        }
        activeWeapon++;
        SendToMainData();
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
            SendToMainData();
            return;
        }
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].gameObject.SetActive(false);
        }
        if (weapons[activeWeapon - 1] != null) {
            weapons[activeWeapon - 1].gameObject.SetActive(true);
        }
        activeWeapon--;
        SendToMainData();
    }
    public int GetCount() {
        return weapons.Count;
    }

    #endregion

    #region WorkingWithWeapon Methods

    public void Attack() {
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].Attack();
        }
    }
    public void ThrowPress() {
        tmpWhenThrowButtonPressed = Time.time;

        Debug.Log(TAG + "Started Timer to Throw");
    }
    public void ThrowRelease() {
        float strenght = throwStrenght;
        float actTime = Time.time;
        if (actTime - tmpWhenThrowButtonPressed < secondsToMaxThrow) {
            strenght *= ((actTime - tmpWhenThrowButtonPressed) / secondsToMaxThrow);
        }
        Debug.Log(TAG + "Throwed with the stenght: " + strenght);
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].Throw(this.gameObject.transform.rotation * Vector2.right * strenght);
            weapons[activeWeapon] = null;
        }
    }
    public void Reload() {
        if (weapons[activeWeapon] != null && weapons[activeWeapon] is Gun) {
            ((Gun)weapons[activeWeapon]).Reload();
        }
    }
    public void ChangeState() {
        if (weapons[activeWeapon] != null) {
            weapons[activeWeapon].ChangeState();
        }
    }

    #endregion

    #region Hand

    public void Aim(Vector3 worldPoint) {
        Vector2 distance = worldPoint - this.transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(distance.y, distance.x);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    #endregion
}
