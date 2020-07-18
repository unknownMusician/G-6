using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Cinemachine;
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

    public RectTransform CardEffectPrefab;
    public RectTransform CardEffectContentOnWeapon;
    public Image CardImageActiveOnWeapon;
    public Text CardNameActiveOnWeapon;
    public Button CardButtonUnInstall;

    #region Action Subsription Managment
    public void Awake()
    {
        MainData.ActionInventoryWeaponsChange += SetAllWeapons;
        SetAllWeapons();
        WeaponClick(MainData.ActiveWeapon);

        MainData.ActionInventoryCardsChange += SetAllCards;
        SetAllCards();
    }

    public void OnDisable()
    {
        MainData.ActionInventoryWeaponsChange -= SetAllWeapons;
        MainData.ActionInventoryCardsChange -= SetAllCards;
    }

    #endregion

    #region Weapons
    private void SetAllWeapons()
    {
        Debug.Log("pidor");
        foreach (RectTransform child in WeaponContent)
        {
            Destroy(child.gameObject);
        }
        if (MainData.Inventory.AllWeapons != null)
        {
            //(ScrollView)WeaponsScrollView.Clear();
            foreach (Weapon weapon in MainData.Inventory.AllWeapons)
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
        MainData.Inventory.ActiveSlot = MainData.Inventory.AllWeapons.IndexOf(activewepon);
        MainData.ActionInventoryActiveSlotChange();
    }

    #endregion

    #region Cards

    private void SetAllCards()
    {
        foreach (RectTransform child in CardContent)
        {
            Destroy(child.gameObject);
        }
        if (MainData.Inventory.Cards != null)
        {

            foreach (Card card in MainData.Inventory.Cards)
            {
                GameObject instanse = GameObject.Instantiate(CardPrefab.gameObject) as GameObject;
                instanse.transform.SetParent(CardContent, false);

                instanse.GetComponent<Button>().onClick.AddListener(delegate
                {
                    CardClick(card, instanse);
                });

                instanse.transform.GetChild(0).gameObject.GetComponent<Text>().text = card.encyclopediaName;
                instanse.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = card.SpriteUI;
            }
        }
    }

    private void CardClick(Card activecard, GameObject instanse)
    {
        instanse.transform.GetChild(2).gameObject.GetComponent<Button>().gameObject.SetActive(true);

        instanse.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            CardInstallClick(activecard);
        });

        CardDescription.text =
            activecard.GetComponent<Card>().encyclopediaDescription;

    }

    private void CardInstallClick(Card activecard)
    {
        if (MainData.ActiveWeapon.InstallUnknownCard(activecard))
        {
            CardViewOnUI(activecard);
            MainData.Inventory.Cards.Remove(activecard);
            MainData.ActionInventoryCardsChange();
        }
    }

    //TODO
    private void UnInstallCardClick(Card card)
    {
        bool isUnInstalling = MainData.ActiveWeapon.UninstallUnknownCard(card);
        if (isUnInstalling)
        {
            MainData.Inventory.Cards.Add(card);
            MainData.ActionInventoryCardsChange();
        }
    }

    #endregion

    #region Button Click View Cards On Weapon

    public void ViewCard1()
    {
        if (MainData.ActiveWeapon is Gun)
        {
            CardViewOnUI(((Gun)MainData.ActiveWeapon).CardEff);
        }
        else
        {
            CardViewOnUI(((Melee)MainData.ActiveWeapon).CardEff);
        }
    }
    public void ViewCard2()
    {
        //if (MainData.ActiveWeapon is Gun)
        //{
        //    CardViewOnUI(((Gun)MainData.ActiveWeapon).CardEff);
        //}
        //else
        //{
        //    CardViewOnUI(((Melee)MainData.ActiveWeapon).CardEff);
        //}
    }
    public void ViewCard3()
    {
        //if (MainData.ActiveWeapon is Gun)
        //{
        //    CardViewOnUI(((Gun)MainData.ActiveWeapon).CardEff);
        //}
        //else
        //{
        //    CardViewOnUI(((Melee)MainData.ActiveWeapon).CardEff);
        //}
    }

    #endregion

    #region HelperMethods

    private void CardViewOnUI(Card card)
    {
        CardImageActiveOnWeapon.sprite = card.SpriteUI;
        CardNameActiveOnWeapon.text = card.encyclopediaName;
        //TODO
        //CardUnInstall
        foreach (KeyValuePair<Sprite, string> module in card.Modules)
        {
            GameObject effectInstanse = Instantiate(CardEffectPrefab.gameObject);
            effectInstanse.transform.SetParent(CardEffectContentOnWeapon, false);
            effectInstanse.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = module.Key;
            effectInstanse.transform.GetChild(1).gameObject.GetComponent<Text>().text = module.Value;
        }
    }

    #endregion
    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
