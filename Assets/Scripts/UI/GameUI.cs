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
    public Image weapons;
    public GameObject menu;

    public static int healthcurrent = 100;
    public static int healtmax = 100;
    public static int endurancecurrent = 100;
    public static int endurancemax = 100;
    public static int patronscurrent = 0;
    public static int patronsmax = 100;
    public static int currentmoney = 0;
    public static Image weaponsimage = null;

    private static Action UpdatInformation;
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
            menu.active = !menu.active;
    }


    public void Start()
    {
        UpdatInformation += UpdateAll;
        //UpdatInformation();
    }

    private void UpdateAll()
    {
        //health
        health.fillRect.GetComponent<Image>().color = Color.red;
        health.maxValue = healtmax;
        health.value = healthcurrent;
        //endurance
        endurance.fillRect.GetComponent<Image>().color = Color.green;
        endurance.maxValue = endurancemax;
        endurance.value = endurancecurrent;
        //money
        money.text = currentmoney.ToString();
        //Patrons
        patrons.text = patronscurrent.ToString() + "/" + patronsmax.ToString();
        //Imageweapons
        weapons = weaponsimage;

    }


    public static void SetHelth(int current, int max)
    {
        healthcurrent = current;
        healtmax = max;
        UpdatInformation();
    }
    public static void SetEndurance(int current, int max)
    {
        endurancecurrent = current;
        endurancemax = max;
        UpdatInformation();
    }
    public void SetMoney(int kmoney)
    {
        currentmoney = kmoney;
        UpdatInformation();
    }

    public void SetPatrons(int current, int max)
    {
        patronscurrent = current;
        patronsmax = max;
        UpdatInformation();
    }

    public void SetImageWeapons(Image image)
    {
        weaponsimage = image;
        UpdatInformation();
    }
}
