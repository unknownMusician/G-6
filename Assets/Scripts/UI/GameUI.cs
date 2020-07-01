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


    private static Action UpdatInformation;

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
            menu.active = !menu.active;
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
        MainData.ActionPatrons += SetPatrons;
        SetPatrons();
        //Imageweapon
        MainData.ActionWeaponSprite += SetImageWeapon;
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
        patrons.text = MainData.ClipPatrons.ToString() + "/" + MainData.OverallPatrons.ToString();

    }

    public void SetImageWeapon()
    {
        if (MainData.CurrentWeaponSprite != null)
            weapon.sprite = MainData.CurrentWeaponSprite;
    }

}
