using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    const string TAG = "Inventory: ";

    #region Card Inventory

    public CardsInventory Cards { get; protected set; }

    protected Transform InventoryCardsMenu => transform.GetChild(1);

    #endregion

    #region Weapon Inventory Properties

    public Weapon Weapon {
        get => WeaponSlots[_activeSlot];
        protected set {
            // To-Do: check if they are same;
            WeaponSlots[_activeSlot] = value;
            MainData.ActionInventoryWeaponsChange?.Invoke();
        }
    }
    public List<Weapon> AllWeapons {
        get {
            var list = new List<Weapon>();
            foreach (var weapon in WeaponSlots) {
                if (weapon != null) {
                    list.Add(weapon);
                }
            }
            return list;
        }
    }

    protected int _activeSlot = 0;
    public int ActiveSlot {
        get => _activeSlot;
        set {
            int fValue = value;
            while (fValue < 0)
                fValue += inventorySize;
            fValue %= inventorySize;

            //if (fValue == _activeSlot)
            //return;
            WeaponSlots[_activeSlot]?.gameObject.SetActive(false);
            WeaponSlots[fValue]?.gameObject.SetActive(true);
            _activeSlot = fValue;

            MainData.ActionInventoryActiveSlotChange?.Invoke();
        }
    }

    protected Transform InventoryWeaponsMenu => transform.GetChild(0);

    #endregion
    protected GameObject Character => transform.parent?.gameObject;
    protected FistFight FistFight { get; set; } = null;
    protected Weapon[] WeaponSlots { get; set; } = new Weapon[4];

    #region Inspector Variables

    [SerializeField] protected int inventorySize = 2;
    [SerializeField] protected float throwStrenght = 25;
    [SerializeField] protected float secondsToMaxThrow = 1;

    #endregion

    protected void Awake() {
        Cards = new CardsInventory(this);
        FistFight = GetComponent<FistFight>();
    }
    protected void Start() {
        CheckChildrenForWeapons();
        CheckChildrenForCards();
        DisableWeaponsExceptFirst();
    }

    #region Character (→ Weapon) Methods

    public void AttackStart() {
        if (Weapon != null)
            Weapon.AttackPress();
        else
            FistFight.Attack();
    }
    public void AttackEnd() => Weapon?.AttackRelease();
    public void ThrowPress() => _tmpWhenThrowButtonPressed = Time.time;
    protected float _tmpWhenThrowButtonPressed;
    public void ThrowRelease() {
        float deltaTime = Time.time - _tmpWhenThrowButtonPressed;
        float strenght = throwStrenght * (
            (deltaTime < secondsToMaxThrow) ? ((deltaTime) / secondsToMaxThrow) : 1f);
        Weapon?.transform.SetParent(null);
        Weapon?.Throw(Character, transform.rotation * Vector2.right * strenght);
        Weapon = null;
    }
    public void ReloadGun() { if (Weapon is Gun gun) gun?.Reload(); }
    public void ChangeWeaponState() => Weapon?.ChangeState();
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

        // Turning the owner
        var cb = GetComponentInParent<CharacterBase>();
        if (cb != null)
            cb.CheckSideLR(localPoint);
    }
    public bool PickUp(Weapon weapon) {
        int newIndex = GetFirstFreeSlotIndex();
        if (newIndex == -1)
            return false;
        // new parent
        weapon.transform.SetParent(InventoryWeaponsMenu);
        weapon.PrepareToPostPickUp();

        // install Weapon
        WeaponSlots[newIndex] = weapon;
        if (newIndex != ActiveSlot)
            WeaponSlots[newIndex].gameObject.SetActive(false);

        return true; // todo
    }
    public bool PickUp(Card card) {
        Cards.Add(card);
        return true; // todo: remove
    }

    #endregion

    #region Service Methods

    protected void CheckChildrenForWeapons() {
        WeaponSlots = new Weapon[4];
        for (int i = 0; i < InventoryWeaponsMenu.childCount; i++) {
            int newIndex = GetFirstFreeSlotIndex();
            if (newIndex == -1)
                break;
            Weapon weapon = InventoryWeaponsMenu.GetChild(i).gameObject.GetComponent<Weapon>();
            if (weapon != null) {
                WeaponSlots[newIndex] = weapon;
                ActiveSlot = newIndex;
                MainData.ActionInventoryWeaponsChange?.Invoke();
            }
        }
        ActiveSlot = 0;
    }
    protected void CheckChildrenForCards() {
        for (int i = 0; i < InventoryCardsMenu.childCount; i++) {
            Card card = InventoryCardsMenu.GetChild(i).gameObject.GetComponent<Card>();
            if (card != null) {
                Cards.Add(card);
                card.transform.position = this.transform.position;
            }
        }
    }

    protected int GetFirstFreeSlotIndex() {
        for (int i = 0; i < inventorySize; i++) {
            if (WeaponSlots[i] == null) {
                return i;
            }
        }
        return -1;
    }

    protected void DisableWeaponsExceptFirst() {
        for (int i = 1; i < InventoryWeaponsMenu.childCount; i++) {
            InventoryWeaponsMenu.GetChild(i)?.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Inner Structures

    public static class Slots {
        readonly public static int FIRST = 0;
        readonly public static int SECOND = 1;
        readonly public static int THIRD = 2;
        readonly public static int FOURTH = 3;
    }
    public enum CoordsType { Local, World, Screen }
    public class CardsInventory {

        protected Transform cardsMenu;
        public CardsInventory(Inventory inventory) {
            this.cardsMenu = inventory.InventoryCardsMenu;
        }

        public void Add(Card item) {
            item.transform.SetParent(cardsMenu);
            MainData.ActionInventoryCardsChange?.Invoke();
        }
        public Card Remove(Card item) {
            Card removed = null;
            int size = cardsMenu.childCount;
            for (int i = 0; i < size; i++) {
                if (cardsMenu.GetChild(i).GetComponent<Card>() == item) {
                    (removed = item).transform.SetParent(null);
                    break;
                }
            }
            MainData.ActionInventoryCardsChange?.Invoke();
            return removed;
        }
        public List<Card> List {
            get {
                List<Card> cards = new List<Card>();
                for (int i = 0; i < cards.Count; i++)
                    cards.Add(cardsMenu.GetChild(i).GetComponent<Card>());
                return new List<Card>(cards);
            }
        }
        public Card Get(int index) => cardsMenu.GetChild(index).GetComponent<Card>();
    }

    #endregion

    [System.Serializable] public class Serialization {

        public CardGunGen.Serialization[] cardsGen;
        public CardGunFly.Serialization[] cardsFly;
        public CardMeleeShape.Serialization[] cardsShape;
        public CardMeleeMemory.Serialization[] cardsMemory;
        public CardEffect.Serialization[] cardsEffect;

        public Gun.Serialization[] guns;
        public Melee.Serialization[] melees;

        protected Serialization(Inventory inv) {
            var cardsGunList = new List<CardGunGen.Serialization>();
            var cardsFlyList = new List<CardGunFly.Serialization>();
            var cardsShapeList = new List<CardMeleeShape.Serialization>();
            var cardsMemoryList = new List<CardMeleeMemory.Serialization>();
            var cardsEffectList = new List<CardEffect.Serialization>();

            var gunsList = new List<Gun.Serialization>();
            var meleesList = new List<Melee.Serialization>();

            foreach (var card in inv.Cards.List) {
                if (card is CardGunGen cardG)
                    cardsGunList.Add(CardGunGen.Serialization.Real2Serializable(cardG));
                else if (card is CardGunFly cardF)
                    cardsFlyList.Add(CardGunFly.Serialization.Real2Serializable(cardF));
                else if (card is CardMeleeShape cardS)
                    cardsShapeList.Add(CardMeleeShape.Serialization.Real2Serializable(cardS));
                else if (card is CardMeleeMemory cardM)
                    cardsMemoryList.Add(CardMeleeMemory.Serialization.Real2Serializable(cardM));
                else if (card is CardEffect cardE)
                    cardsEffectList.Add(CardEffect.Serialization.Real2Serializable(cardE));
            }

            cardsGen = cardsGunList.ToArray();
            cardsFly = cardsFlyList.ToArray();
            cardsShape = cardsShapeList.ToArray();
            cardsMemory = cardsMemoryList.ToArray();
            cardsEffect = cardsEffectList.ToArray();

            foreach (var weapon in inv.AllWeapons) {
                if (weapon is Gun gun)
                    gunsList.Add(Gun.Serialization.Real2Serializable(gun));
                else if (weapon is Melee melee)
                    meleesList.Add(Melee.Serialization.Real2Serializable(melee));
            }

            guns = gunsList.ToArray();
            melees = meleesList.ToArray();
        }

        public static Serialization Real2Serializable(Inventory inv) { return new Serialization(inv); }

        public static void Serializable2Real(Serialization serialization, Inventory inv) {

            // cards
            var cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardGunGen.prefab");
            foreach(var card in serialization.cardsGen) {
                var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardGunGen>();
                CardGunGen.Serialization.Serializable2Real(card, cardObjectComponent);
                inv.Cards.Add(cardObjectComponent);
            }
            cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardGunFly.prefab");
            foreach (var card in serialization.cardsFly) {
                var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardGunFly>();
                CardGunFly.Serialization.Serializable2Real(card, cardObjectComponent);
                inv.Cards.Add(cardObjectComponent);
            }
            cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardMeleeShape.prefab");
            foreach (var card in serialization.cardsShape) {
                var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardMeleeShape>();
                CardMeleeShape.Serialization.Serializable2Real(card, cardObjectComponent);
                inv.Cards.Add(cardObjectComponent);
            }
            cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardMeleeMemory.prefab");
            foreach (var card in serialization.cardsMemory) {
                var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardMeleeMemory>();
                CardMeleeMemory.Serialization.Serializable2Real(card, cardObjectComponent);
                inv.Cards.Add(cardObjectComponent);
            }
            cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardEffect.prefab");
            foreach (var card in serialization.cardsEffect) {
                var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardEffect>();
                CardEffect.Serialization.Serializable2Real(card, cardObjectComponent);
                inv.Cards.Add(cardObjectComponent);
            }

            // weapons
            var weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Guns/Pistol.prefab");
            foreach (var gun in serialization.guns) {
                var gunObjectComponent = Instantiate(weaponPrefab).GetComponent<Gun>();
                Gun.Serialization.Serializable2Real(gun, gunObjectComponent);
                inv.PickUp(gunObjectComponent); // todo: check PickUp()
            }
            weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Melees/Knife.prefab");
            foreach (var melee in serialization.melees) {
                var meleeObjectComponent = Instantiate(weaponPrefab).GetComponent<Melee>();
                Melee.Serialization.Serializable2Real(melee, meleeObjectComponent);
                inv.PickUp(meleeObjectComponent); // todo: check PickUp()
            }
            // todo
        }
    }
}
