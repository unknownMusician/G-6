using G6.Characters;
using G6.Data;
using G6.Weapons.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace G6.Weapons {
    public class Inventory : MonoBehaviour {

        const string TAG = "Inventory: ";

        #region Inventories

        public CardsInventory Cards { get; protected set; }
        public WeaponsInventory Weapons { get; protected set; }

        #endregion
        protected GameObject Character => transform.parent?.gameObject;
        protected FistFight FistFight { get; set; } = null;

        #region Inspector Variables

        #endregion

        protected Inventory() {
            Cards = new CardsInventory(this);
            Weapons = new WeaponsInventory(this);
        }
        protected void Awake() {
            FistFight = GetComponent<FistFight>();
        }
        protected void Start() {
            Weapons.CheckChildrenForWeapons();
            Cards.CheckChildrenForCards();
            Weapons.DisableWeaponsExceptFirst();
        }

        #region Character (→ Weapon) Methods

        public void AttackStart() {
            if (Weapons.Weapon != null)
                Weapons.Weapon.AttackPress();
            else
                FistFight.Attack();
        }
        public void AttackEnd() => Weapons.Weapon?.AttackRelease();
        public void ThrowPress() => Weapons.ThrowPress();
        public void ThrowRelease() => Weapons.ThrowRelease();
        public void ReloadGun() => Weapons.ReloadGun();
        public void ChangeWeaponState() => Weapons.ChangeWeaponState();
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
        public bool PickUp(Weapon weapon) => Weapons.Add(weapon);
        public bool PickUp(Card card) => Cards.Add(card);

        #endregion

        #region Service Methods

        #endregion

        #region Inner Structures

        public static class Slots {
            readonly public static int FIRST = 0;
            readonly public static int SECOND = 1;
            readonly public static int THIRD = 2;
            readonly public static int FOURTH = 3;
        }
        public enum CoordsType { Local, World, Screen }
        [System.Serializable]
        public class CardsInventory {
            // Values
            protected Inventory inv = null;
            protected Transform InventoryCardsMenu => inv.transform.GetChild(1);

            public CardsInventory(Inventory inventory) {
                this.inv = inventory;
            }

            // List methods
            public bool Add(Card item) {
                item.transform.SetParent(InventoryCardsMenu);
                MainData.ActionInventoryCardsChange?.Invoke();
                return true; // todo
            }
            public Card Remove(Card item) {
                Card removed = null;
                int size = InventoryCardsMenu.childCount;
                for (int i = 0; i < size; i++) {
                    if (InventoryCardsMenu.GetChild(i).GetComponent<Card>() == item) {
                        (removed = item).transform.SetParent(null);
                        break;
                    }
                }
                MainData.ActionInventoryCardsChange?.Invoke();
                return removed;
            }
            public void Clear() {
                int size = InventoryCardsMenu.childCount;
                for (int i = 0; i < size; i++) {
                    Destroy(InventoryCardsMenu.GetChild(0).gameObject);
                }
                MainData.ActionInventoryCardsChange?.Invoke();
            }
            public List<Card> List {
                get {
                    List<Card> cards = new List<Card>();
                    for (int i = 0; i < cards.Count; i++)
                        cards.Add(InventoryCardsMenu.GetChild(i).GetComponent<Card>());
                    return new List<Card>(cards);
                }
            }
            public Card Get(int index) => InventoryCardsMenu.GetChild(index).GetComponent<Card>();

            // Service methods
            public void CheckChildrenForCards() {
                for (int i = 0; i < InventoryCardsMenu.childCount; i++) {
                    Card card = InventoryCardsMenu.GetChild(i).gameObject.GetComponent<Card>();
                    if (card != null) {
                        Add(card);
                        card.transform.position = inv.transform.position;
                    }
                }
            }
        }
        [System.Serializable]
        public class WeaponsInventory {

            // Inspector values
            [SerializeField] protected int inventorySize = 2;
            [SerializeField] protected float throwStrenght = 25;
            [SerializeField] protected float secondsToMaxThrow = 1;

            #region Values

            protected Inventory inv = null;
            protected Transform InventoryWeaponsMenu => inv.transform.GetChild(0);
            protected Weapon[] WeaponSlots { get; set; } = new Weapon[4];

            public Weapon Weapon {
                get => WeaponSlots[ActiveSlot];
                set {
                    // To-Do: check if they are same;
                    WeaponSlots[ActiveSlot] = value;
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
            #endregion

            public WeaponsInventory(Inventory inventory) => this.inv = inventory;

            // List Methods
            public bool Add(Weapon weapon) {
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
            public void Clear() {
                // todo
                for (int i = 0; i < WeaponSlots.Length; i++) {
                    if (WeaponSlots[i] != null) { Destroy(WeaponSlots[i].gameObject); }
                    WeaponSlots[i] = null;
                }
                ActiveSlot = 0;
                MainData.ActionInventoryWeaponsChange?.Invoke();
                MainData.ActionInventoryActiveSlotChange?.Invoke();
            }

            #region Acting methods

            public void ThrowPress() => _tmpWhenThrowButtonPressed = Time.time;
            protected float _tmpWhenThrowButtonPressed;
            public void ThrowRelease() {
                float deltaTime = Time.time - _tmpWhenThrowButtonPressed;
                float strenght = throwStrenght * (
                    (deltaTime < secondsToMaxThrow) ? ((deltaTime) / secondsToMaxThrow) : 1f);
                Weapon?.transform.SetParent(null);
                Weapon?.Throw(inv.Character, inv.transform.rotation * Vector2.right * strenght);
                Weapon = null;
            }
            public void ReloadGun() { if (Weapon is Gun gun) gun?.Reload(); }
            public void ChangeWeaponState() => Weapon?.ChangeState();
            #endregion

            #region Service Methods

            public void CheckChildrenForWeapons() {
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

            public int GetFirstFreeSlotIndex() {
                for (int i = 0; i < inventorySize; i++) {
                    if (WeaponSlots[i] == null) {
                        return i;
                    }
                }
                return -1;
            }

            public void DisableWeaponsExceptFirst() {
                for (int i = 1; i < InventoryWeaponsMenu.childCount; i++) {
                    InventoryWeaponsMenu.GetChild(i)?.gameObject.SetActive(false);
                }
            }
            #endregion
        }

        #endregion

        [System.Serializable]
        public class Serialization {

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

                foreach (var weapon in inv.Weapons.AllWeapons) {
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

                // clear Inventory at first
                inv.Cards.Clear();
                inv.Weapons.Clear();

                // cards
                var cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardGunGen");
                foreach (var card in serialization.cardsGen) {
                    var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardGunGen>();
                    CardGunGen.Serialization.Serializable2Real(card, cardObjectComponent);
                    inv.Cards.Add(cardObjectComponent);
                }
                cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardGunFly");
                foreach (var card in serialization.cardsFly) {
                    var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardGunFly>();
                    CardGunFly.Serialization.Serializable2Real(card, cardObjectComponent);
                    inv.Cards.Add(cardObjectComponent);
                }
                cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardMeleeShape");
                foreach (var card in serialization.cardsShape) {
                    var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardMeleeShape>();
                    CardMeleeShape.Serialization.Serializable2Real(card, cardObjectComponent);
                    inv.Cards.Add(cardObjectComponent);
                }
                cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardMeleeMemory");
                foreach (var card in serialization.cardsMemory) {
                    var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardMeleeMemory>();
                    CardMeleeMemory.Serialization.Serializable2Real(card, cardObjectComponent);
                    inv.Cards.Add(cardObjectComponent);
                }
                cardPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Cards/CardEffect");
                foreach (var card in serialization.cardsEffect) {
                    var cardObjectComponent = Instantiate(cardPrefab).GetComponent<CardEffect>();
                    CardEffect.Serialization.Serializable2Real(card, cardObjectComponent);
                    inv.Cards.Add(cardObjectComponent);
                }

                // weapons
                var weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Guns/Pistol");
                foreach (var gun in serialization.guns) {
                    var gunObjectComponent = Instantiate(weaponPrefab).GetComponent<Gun>();
                    Gun.Serialization.Serializable2Real(gun, gunObjectComponent);
                    inv.Weapons.Add(gunObjectComponent);
                }
                weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Melees/Knife");
                foreach (var melee in serialization.melees) {
                    var meleeObjectComponent = Instantiate(weaponPrefab).GetComponent<Melee>();
                    Melee.Serialization.Serializable2Real(melee, meleeObjectComponent);
                    inv.Weapons.Add(meleeObjectComponent);
                }
                // todo
            }
        }
    }
}