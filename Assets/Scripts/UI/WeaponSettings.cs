using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WeaponSettings : MonoBehaviour
{
    public RectTransform WeaponButtonPrefab;
    public RectTransform WeaponContent;
    public Image WeaponMainImage;
    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI WeaponDescription;
    [Space]
    public RectTransform CardPrefab;
    public RectTransform CardContent;
    public TextMeshProUGUI CardDescription;
    [Space]
    public RectTransform CardEffectPrefab;
    public RectTransform CardEffectContentOnWeapon;
    public Image CardImageActiveOnWeapon;
    public TextMeshProUGUI CardNameActiveOnWeapon;
    public Button CardButtonUnInstall;

    private GameObject SelectCard;

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

                instanse.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = card.encyclopediaName;
                instanse.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = card.gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    private void CardClick(Card activecard, GameObject instanse)
    {
        if (SelectCard)
            SelectCard.transform.GetChild(2).gameObject.GetComponent<Button>().gameObject.SetActive(false);

        instanse.transform.GetChild(2).gameObject.GetComponent<Button>().gameObject.SetActive(true);
        instanse.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            CardInstallClick(activecard);
        });
        CardDescription.text =
            activecard.GetComponent<Card>().encyclopediaDescription;

        SelectCard = instanse;
    }

    private void CardInstallClick(Card activecard)
    {
        if (MainData.ActiveWeapon.InstallUnknownCard(activecard))
        {
            CardViewOnUI(activecard);
        }
    }

    //TODO
    private void UnInstallCardClick(Card card)
    {
        if (MainData.ActiveWeapon.UninstallUnknownCard(card))
        {
            CardButtonUnInstall.onClick.RemoveAllListeners();
            CardNullViewOnUI();
        }
    }

    #endregion

    #region Button Click View Cards On Weapon

    public void ViewCard1()
    {
        CardButtonUnInstall.onClick.RemoveAllListeners();
        if (MainData.ActiveWeapon is Gun)
            CardViewOnUI(((Gun)MainData.ActiveWeapon).CardGen);
        else
            CardViewOnUI(((Melee)MainData.ActiveWeapon).CardShape);
    }
    public void ViewCard2()
    {
        CardButtonUnInstall.onClick.RemoveAllListeners();
        if (MainData.ActiveWeapon is Gun)
            CardViewOnUI(((Gun)MainData.ActiveWeapon).CardFly);
        else
            CardViewOnUI(((Melee)MainData.ActiveWeapon).CardMemory);
    }
    public void ViewCard3()
    {
        CardButtonUnInstall.onClick.RemoveAllListeners();
        if (MainData.ActiveWeapon is Gun)
            CardViewOnUI(((Gun)MainData.ActiveWeapon).CardEff);
        else
            CardViewOnUI(((Melee)MainData.ActiveWeapon).CardEff);
    }

    #endregion

    #region HelperMethods

    private void CardViewOnUI(Card card)
    {
        if (card != null)
        {
            CardImageActiveOnWeapon.sprite = card.SpriteUI;
            CardNameActiveOnWeapon.text = card.encyclopediaName;
            CardButtonUnInstall.onClick.RemoveAllListeners();
            CardButtonUnInstall.onClick.AddListener(delegate { UnInstallCardClick(card); });

            foreach (RectTransform effect in CardEffectContentOnWeapon)
            {
                Destroy(effect.gameObject);
            }

            foreach (KeyValuePair<Sprite, string> module in card.Modules)
            {
                GameObject effectInstanse = Instantiate(CardEffectPrefab.gameObject);
                effectInstanse.transform.SetParent(CardEffectContentOnWeapon, false);
                effectInstanse.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = module.Key;
                effectInstanse.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = module.Value;
            }
        }
        else
        {
            CardNullViewOnUI();
        }
    }

    private void CardNullViewOnUI()
    {
        CardImageActiveOnWeapon.sprite = null;
        CardNameActiveOnWeapon.text = "";
        foreach (RectTransform effect in CardEffectContentOnWeapon)
        {
            Destroy(effect.gameObject);
        }
    }

    #endregion

}
