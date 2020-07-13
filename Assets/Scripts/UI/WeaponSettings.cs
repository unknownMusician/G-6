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
        MainData.ActionWeapons += SetWeapons;
    }
    public void DisActiveWeaponSettings()
    {
        MainData.ActionWeapons -= SetWeapons;
    }

    public void Awake()
    {
        SetWeapons();
        WeaponClick(MainData.ActiveWeapon);
        SetAllCards();
    }

    private void SetWeapons()
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
            var instance = GameObject.Instantiate(WeaponButtonPrefab.gameObject) as GameObject;
            instance.transform.SetParent(WeaponContent, false);
            instance.GetComponent<Image>().sprite =
                weapon.WeaponPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            instance.GetComponent<Button>().onClick.AddListener(delegate { WeaponClick(weapon); });
        }
    }

    private void WeaponClick(Weapon.NestedInfo activewepon)
    {
        WeaponMainImage.sprite = activewepon.WeaponPrefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        WeaponName.text = activewepon.WeaponPrefab.GetComponent<Weapon>().encyclopediaName;
        WeaponDescription.text = activewepon.WeaponPrefab.GetComponent<Weapon>().encyclopediaDescription;
        MainData.ActiveWeaponIndex = MainData.InventoryWeapons.FindIndex(((i) => i == activewepon));
    }

    private void SetAllCards()
    {

    }
    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
