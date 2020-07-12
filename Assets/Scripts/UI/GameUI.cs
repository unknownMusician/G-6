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


    private static Action UpdatInformation;

    void Update()
    {
        if (Input.GetButtonDown("Setting"))
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
        if (Input.GetButtonDown("WeaponSettings"))
        {
            if (PauseMenu.GameIsPaused)
            {
                PauseMenu.GameIsPaused = false;
                weaponSettings.SetActive(false);
                weaponSettings.gameObject.GetComponent<WeaponSettings>().DisActiveWeaponSettings();

            }
            else
            {
                PauseMenu.GameIsPaused = true;
                weaponSettings.SetActive(true);
                weaponSettings.gameObject.GetComponent<WeaponSettings>().ActiveWeaponSettings();
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
        MainData.ActionHP += SetHelth;
        health.fillRect.GetComponent<Image>().color = Color.red;
        SetHelth();
        //endurance
        MainData.ActionHP += SetEndurance;
        endurance.fillRect.GetComponent<Image>().color = Color.green;
        SetEndurance();
        //money
        MainData.ActionMoney += SetMoney;
        SetMoney();
        //Patrons
        MainData.ActionWeapons += SetPatrons;
        SetPatrons();
        //Imageweapon
        MainData.ActionWeapons += SetImageWeapon;
        SetImageWeapon();
    }

    public void SetHelth()
    {
        health.maxValue = MainData.OverallHP;
        health.value = MainData.CurrentHP;
    }
    public void SetEndurance()
    {
        endurance.maxValue = MainData.OverallHP;
        endurance.value = MainData.CurrentHP;
    }
    public void SetMoney()
    {
        money.text = "Money:" + MainData.CurrentMoney.ToString();

    }

    public void SetPatrons()
    {
        if (MainData.ActiveWeapon is Gun.Info)
            patrons.text = ((Gun.Info)MainData.ActiveWeapon).ActualClipBullets.ToString() + "/" + ((Gun.Info)MainData.ActiveWeapon).ActualPocketBullets.ToString();
        else
            patrons.text = "0/0";
    }

    public void SetImageWeapon()
    {
        if (MainData.ActiveWeapon != null)
            weapon.sprite = MainData.ActiveWeapon.WeaponPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

}
