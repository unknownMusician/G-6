using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    const string TAG = "Inventory: ";

    #region Card Inventory

    private List<Card> cards = new List<Card>();
    public List<Card> Cards {
        get => cards;
        set {
            // To-Do: check if they are same;
            cards = value;
            foreach (var card in cards)
                card.gameObject.transform.SetParent(inventoryCardsFolder);
            MainData.ActionInventoryCardsChange?.Invoke();
        }
    }

    private void GetCardsFromChildren() {
        for (int i = 0; i < inventoryCardsFolder.childCount; i++) {
            Card card = inventoryCardsFolder.GetChild(i).gameObject.GetComponent<Card>();
            if (card != null) {
                Cards.Add(card);
                card.transform.position = this.transform.position;
            }
        }
        Cards = Cards;
    }

    #endregion

    #region Properties

    public Weapon Weapon {
        get => weapons[activeWeapon];
        private set {
            // To-Do: check if they are same;
            weapons[activeWeapon] = value;
            MainData.ActionInventoryWeaponsChange?.Invoke();
        }
    }

    public List<Weapon> AllWeapons {
        get {
            var list = new List<Weapon>();
            foreach (var weapon in weapons) {
                if (weapon != null) {
                    list.Add(weapon);
                }
            }
            return list;
        }
    }

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

            MainData.ActionInventoryActiveSlotChange?.Invoke();
        }
    }

    private GameObject Character => transform.parent?.gameObject;

    #endregion

    #region Public Variables

    [SerializeField]
    private FistFight fistFight = null;

    [Space]
    [SerializeField]
    private List<Weapon> weapons = null;
    [SerializeField]
    private Transform inventoryCardsFolder = null;
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
        GetCardsFromChildren();
    }

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

    public void AttackWithWeaponOrFistStart() {
        if (Weapon != null)
            Weapon.AttackPress();
        else
            fistFight.Attack();
    }
    public void AttackWithWeaponOrFistEnd() {
        if (Weapon != null)
            Weapon.AttackRelease();
    }
    public void ThrowPress() {
        tmpWhenThrowButtonPressed = Time.time;

        Debug.Log(TAG + "Started Timer to Throw");
    }
    public void ThrowRelease() {
        float actTime = Time.time;
        float strenght = throwStrenght * (
            (actTime - tmpWhenThrowButtonPressed < secondsToMaxThrow) ? ((actTime - tmpWhenThrowButtonPressed) / secondsToMaxThrow) : 1f);
        Weapon?.Throw(gameObject, gameObject.transform.rotation * Vector2.right * strenght);
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

    public void Aim(Vector3 point, CoordsType type) {
        var localPoint = point;

        if (type == CoordsType.World) {
            localPoint -= Character.transform.position;
        } else if (type == CoordsType.Screen) {
            localPoint = Camera.main.ScreenToWorldPoint(point) - Character.transform.position;
        }

        float angle = Mathf.Rad2Deg * Mathf.Atan2(localPoint.y, localPoint.x);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);

        //
        transform.localScale = new Vector3(1, Mathf.Sign(localPoint.x), 1);
    }

    public enum CoordsType { Local, World, Screen }

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
