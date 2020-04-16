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
    public Image mapimg;
    public GameObject mapGameObject;
    public GameObject menu;

    private static int healthcurrent = 100;
    private static int healtmax = 100;
    private static int endurancecurrent = 100;
    private static int endurancemax = 100;
    private static int patronscurrent = 0;
    private static int patronsmax = 100;
    private static int currentmoney = 0;
    private static Image weaponsimage = null;
    private static Image mapimage;

    private static Action UpdatInformation;

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
            menu.active = !menu.active;
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.M.ToString())))
            mapGameObject.active = !mapGameObject.active;
    }


    public void Start()
    {
        UpdatInformation += UpdateAll;
        UpdatInformation();
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
        money.text = "Money:" + currentmoney.ToString();
        //Patrons
        patrons.text = patronscurrent.ToString() + "/" + patronsmax.ToString();
        //Imageweapons
        weapons = weaponsimage;
        //Map
        mapimg = mapimage;

    }

    public void OpenCloseMap()
    {
        mapGameObject.active = !mapGameObject.active;
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
    public static void SetMoney(int kmoney)
    {
        currentmoney = kmoney;
        UpdatInformation();
    }

    public static void SetPatrons(int current, int max)
    {
        patronscurrent = current;
        patronsmax = max;
        UpdatInformation();
    }

    public static void SetImageWeapons(Image image)
    {
        weaponsimage = image;
        UpdatInformation();
    }

    public static void SetMap(Image image)
    {
        mapimage = image;
        UpdatInformation();
    }
}
