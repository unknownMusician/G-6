using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WeaponSettings : MonoBehaviour
{
    public RectTransform WeaponButtonPrefab;
    public RectTransform WeaponContent;
    public Image WeaponMainImage;
    public Text WeaponName;
    public Text WeaponDescription;

    public RectTransform CardPrefab;
    public RectTransform CardContent;
    public Text CardDescription;

    public void ActiveWeaponSettings()
    {
        MainData.ActionWeapons += SetWeaponSetings;
    }
    public void DisActiveWeaponSettings()
    {
        MainData.ActionWeapons -= SetWeaponSetings;
    }

    public void Awake()
    {
        SetWeaponSetings();
        WeaponClick(MainData.ActiveWeapon);
        SetAllCards();
    }

    private void SetWeaponSetings()
    {
        Debug.Log("Pidor");
        SetImageToScrollView();
    }

    private void SetImageToScrollView()
    {
        foreach (RectTransform child in WeaponContent)
        {
            Destroy(child.gameObject);
        }
        //(ScrollView)WeaponsScrollView.Clear();
        foreach (Weapon.NestedInfo weapon in MainData.InventoryWeapons)
        {
            GameObject instance = GameObject.Instantiate(WeaponButtonPrefab.gameObject) as GameObject;
            instance.transform.SetParent(WeaponContent, false);
            instance.GetComponent<Image>().sprite =
                weapon.Prefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            instance.GetComponent<Button>().onClick.AddListener(delegate { WeaponClick(weapon); });
        }
    }

    private void WeaponClick(Weapon.NestedInfo activewepon)
    {
        WeaponMainImage.sprite = activewepon.Prefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        WeaponName.text = activewepon.Prefab.GetComponent<Weapon>().encyclopediaName;
        WeaponDescription.text = activewepon.Prefab.GetComponent<Weapon>().encyclopediaDescription;
        MainData.ActiveWeaponIndex = MainData.InventoryWeapons.FindIndex(((i) => i == activewepon));
    }

    private void SetAllCards()
    {
        foreach (RectTransform child in CardContent)
        {
            Destroy(child);
        }

        foreach (Card.NestedInfo card in MainData.InventoryCards)
        {
            GameObject instanse = GameObject.Instantiate(CardPrefab.gameObject) as GameObject;
            instanse.transform.SetParent(CardContent, false);
            instanse.GetComponent<Button>().onClick.AddListener(delegate { CardClick(card); });
        }
    }

    private void CardClick(Card.NestedInfo activecard)
    {
        //instanse.transform.GetChild(2).gameObject.GetComponent<Button>()

    }

    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
