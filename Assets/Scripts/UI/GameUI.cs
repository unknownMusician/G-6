using G6.Data;
using G6.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace G6.UI {
    public class GameUI : MonoBehaviour
    {
        public static GameUI instance { get; set; } = default; // todo ? (remove)
        protected void Awake() => instance = this;
        protected void OnDestroy() => instance = null;

        public Text money = default; // todo: TextMeshPro
        public Text patrons = default; // todo: TextMeshPro
        public Slider health = default;
        public Slider endurance = default;
        public Image weapon = default;


        //private bool p = true; // todo remove

        public void LoadSetting()
        {
            Setting.instance.gameObject.SetActive(true);
            Menu.instance.gameObject.SetActive(false);
        }

        protected void OnEnable() {
            //health
            MainData.ActionHPChange += SetHealth;
            //endurance
            MainData.ActionSPChange += SetEndurance;
            //money
            MainData.ActionPlayerCoinsChange += SetMoney;
            //Patrons
            MainData.ActionGunBulletsChange += SetPatrons;
            //Imageweapon
            MainData.ActionInventoryActiveSlotChange += SetImageWeapon;
            MainData.ActionInventoryWeaponsChange += SetImageWeapon;
        }
        protected void OnDisable() {
            //health
            MainData.ActionHPChange -= SetHealth;
            //endurance
            MainData.ActionSPChange -= SetEndurance;
            //money
            MainData.ActionPlayerCoinsChange -= SetMoney;
            //Patrons
            MainData.ActionGunBulletsChange -= SetPatrons;
            //Imageweapon
            MainData.ActionInventoryActiveSlotChange -= SetImageWeapon;
            MainData.ActionInventoryWeaponsChange -= SetImageWeapon;
        }

        public void Start()
        {
            //health
            health.fillRect.GetComponent<Image>().color = Color.red;
            //endurance
            endurance.fillRect.GetComponent<Image>().color = Color.green;
        }

        public void SetHealth()
        {
            health.maxValue = MainData.PlayerMaxHP;
            health.value = MainData.PlayerHP;
        }
        public void SetEndurance()
        {
            endurance.maxValue = MainData.PlayerMaxSP;
            endurance.value = MainData.PlayerSP;
        }
        public void SetMoney()
        {
            money.text = "Money:" + MainData.PlayerCoins.ToString();
        }

        public void SetPatrons()
        {
            if (MainData.ActiveWeapon is Gun)
                patrons.text = ((Gun)MainData.ActiveWeapon).ActualClipBullets.ToString() + "/" + ((Gun)MainData.ActiveWeapon).ActualPocketBullets.ToString();
            else
                patrons.text = "0/0";
        }

        public void SetImageWeapon()
        {
            if (MainData.ActiveWeapon != null)
                weapon.sprite = MainData.ActiveWeapon.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
}