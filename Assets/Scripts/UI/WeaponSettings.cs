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
        MainData.ActionWeapons += SetImageToScrollView;
        MainData.ActionInventoryCards += SetAllCards;
    }
    public void DisActiveWeaponSettings()
    {
        MainData.ActionWeapons -= SetImageToScrollView;
        MainData.ActionInventoryCards -= SetAllCards;

    }

    public void Awake()
    {
        SetImageToScrollView();
        WeaponClick(MainData.ActiveWeapon);

        SetAllCards();
    }



    private void SetImageToScrollView()
    {
        foreach (RectTransform child in WeaponContent)
        {
            Destroy(child.gameObject);
        }
        if (MainData.InventoryWeapons != null)
        {
            //(ScrollView)WeaponsScrollView.Clear();
            foreach (Weapon weapon in MainData.InventoryWeapons)
            {
                GameObject instance = GameObject.Instantiate(WeaponButtonPrefab.gameObject) as GameObject;
                instance.transform.SetParent(WeaponContent, false);
                instance.GetComponent<Image>().sprite =
                    weapon.GetComponent<SpriteRenderer>().sprite;
                instance.GetComponent<Button>().onClick.AddListener(delegate { WeaponClick(weapon); });
            }
        }
    }

    private void WeaponClick(Weapon activewepon)
    {
        WeaponMainImage.sprite = activewepon.GetComponentInChildren<SpriteRenderer>().sprite;
        WeaponName.text = activewepon.GetComponent<Weapon>().encyclopediaName;
        WeaponDescription.text = activewepon.GetComponent<Weapon>().encyclopediaDescription;
        MainData.ActiveWeaponIndex = MainData.InventoryWeapons.FindIndex(((i) => i == activewepon));
    }

    private void SetAllCards()
    {
        foreach (RectTransform child in CardContent)
        {
            Destroy(child);
        }
        if (MainData.InventoryCards != null)
        {

            foreach (Card card in MainData.InventoryCards)
            {
                GameObject instanse = GameObject.Instantiate(CardPrefab.gameObject) as GameObject;
                instanse.transform.SetParent(CardContent, false);

                instanse.GetComponent<Button>().onClick.AddListener(delegate { CardClick(card, instanse); });

                instanse.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                    card.Prefab.gameObject.GetComponent<SpriteRenderer>().sprite;

                instanse.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                    card.Prefab.GetComponent<Card>().encyclopediaName;


            }
        }
    }

    private void CardClick(Card activecard, GameObject instanse)
    {
        instanse.transform.GetChild(2).gameObject.GetComponent<Button>().gameObject.SetActive(true);

        CardDescription.text =
            activecard.Prefab.GetComponent<Card>().encyclopediaDescription;

    }

    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
