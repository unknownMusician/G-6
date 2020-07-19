using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Text money;
    public Text patrons;
    public Slider health;
    public Slider endurance;
    public Image weapon;
    public GameObject menu;
    public GameObject setting;
    public GameObject weaponSettings;


    //private bool p = true;

    void Update()
    {
        if (Input.GetButtonDown("Setting") && !weaponSettings.activeInHierarchy)
        {
            if (PauseMenu.GameIsPaused)
            {
                menu.SetActive(false);
                PauseMenu.GameIsPaused = false;
            }
            else
            {
                PauseMenu.GameIsPaused = true;
                menu.SetActive(true);
            }

        }
        if (Input.GetButtonDown("WeaponSettings") && !menu.activeInHierarchy)
        {
            if (PauseMenu.GameIsPaused)
            {
                PauseMenu.GameIsPaused = false;
                weaponSettings.SetActive(false);
            }
            else
            {
                PauseMenu.GameIsPaused = true;
                weaponSettings.SetActive(true);
            }
        }
    }

    public void LoadSetting()
    {

        setting.SetActive(true);
        menu.SetActive(false);
    }

    public void Start()
    {
        //health
        MainData.ActionHPChange += SetHelth;
        health.fillRect.GetComponent<Image>().color = Color.red;
        //endurance
        MainData.ActionHPChange += SetEndurance;
        endurance.fillRect.GetComponent<Image>().color = Color.green;
        //money
        MainData.ActionPlayerCoinsChange += SetMoney;
        //Patrons
        MainData.ActionGunBulletsChange += SetPatrons;
        //Imageweapon
        MainData.ActionInventoryActiveSlotChange += SetImageWeapon;
        MainData.ActionInventoryWeaponsChange += SetImageWeapon;
    }

    public void SetHelth()
    {
        health.maxValue = MainData.PlayerMaxHP;
        health.value = MainData.PlayerHP;
    }
    public void SetEndurance()
    {
        endurance.maxValue = MainData.PlayerMaxHP;
        endurance.value = MainData.PlayerHP;
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
