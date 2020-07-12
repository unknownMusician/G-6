using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    const string TAG = "Inventory: ";

    #region Card Inventory

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

    #endregion

    #region Properties

    public Weapon Weapon { get => weapons[activeWeapon]; private set => weapons[activeWeapon] = value; }

    public int ActiveSlot {
        get => activeWeapon;
        set {
            int fValue = value;
            while (fValue < 0)
                fValue += weapons.Count;
            fValue %= weapons.Count;

            if (fValue == activeWeapon) {
                return;
            }
            weapons[activeWeapon]?.gameObject.SetActive(false);
            weapons[fValue]?.gameObject.SetActive(true);
            activeWeapon = fValue;

            MainData.ActiveWeaponIndex = ActiveSlot; // Sending to ActiveSlot to MainData
        }
    }

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

    private int activeWeapon = 0; // index
    protected float tmpWhenThrowButtonPressed;

    #endregion

    #region Overrided Methods

    private void Start() {
        GetWeaponsFromChildren();
        SendInventoryWeaponsToMainData();
        MainData.ActionWeapons += ReceiveActiveWeaponIndexFromMainData;
    }

    #endregion

    #region MainData Methods

    private void SendInventoryWeaponsToMainData() {
        List<Weapon.NestedInfo> allWeapons = new List<Weapon.NestedInfo>();
        for (int i = 0; i < weapons.Count; i++) {
            allWeapons.Add(weapons[i] == null ? null : ((weapons[i] is Gun gun) ? gun.Info : weapons[i].Info));
        }
        MainData.InventoryWeapons = allWeapons;
    }

    ////////

    public void ReceiveActiveWeaponIndexFromMainData() => ActiveSlot = MainData.ActiveWeaponIndex;

    #endregion

    #region WorkingWithSlots Methods

    private void GetWeaponsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Weapon weapon = this.transform.GetChild(i).gameObject.GetComponent<Weapon>();
            if (weapon != null) {
                for (int j = 0; j < weapons.Count; j++) {
                    if (weapons[j] == null) {
                        weapons[j] = weapon;
                        ActiveSlot = j;
                        break;
                    }
                }
            }
        }
        ActiveSlot = 0;
    }
    public void Choose(int index) {
        ActiveSlot = index;
    }
    public void ChooseNext() {
        ActiveSlot++;
    }
    public void ChoosePrev() {
        ActiveSlot--;
    }

    #endregion

    #region WorkingWithWeapon Methods

    public void AttackWithWeapon() {
        Weapon?.Attack();
    }
    public void ThrowPress() {
        tmpWhenThrowButtonPressed = Time.time;

        Debug.Log(TAG + "Started Timer to Throw");
    }
    public void ThrowRelease() {
        float actTime = Time.time;
        float strenght = throwStrenght * (
            (actTime - tmpWhenThrowButtonPressed < secondsToMaxThrow) ? ((actTime - tmpWhenThrowButtonPressed) / secondsToMaxThrow) : 1f);
        Weapon?.Throw(this.gameObject, this.gameObject.transform.rotation * Vector2.right * strenght);
        Weapon = null;

        Debug.Log(TAG + "Throwed with the stenght: " + strenght);
    }
    public void ReloadGun() {
        if (Weapon is Gun gun) {
            gun?.Reload();
        }
    }
    public void ChangeWeaponState() {
        Weapon?.ChangeState();
    }

    #endregion

    #region Aim

    public void Aim(Vector3 worldPoint) {
        Vector2 distance = worldPoint - this.transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(distance.y, distance.x);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    #endregion

    #region Inner Structures

    public static class Slots {
        readonly public static int FIRST = 0;
        readonly public static int SECOND = 1;
        readonly public static int THIRD = 2;
        readonly public static int FOURTH = 3;
    }

    #endregion
}
