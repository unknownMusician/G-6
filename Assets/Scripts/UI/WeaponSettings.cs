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
    public TextMeshProUGUI CardNameActiveOnWeapon;
    public Button CardButtonUnInstall;
    [Space]
    public Image CardImageOnWeapon1;
    public Image CardImageOnWeapon2;
    public Image CardImageOnWeapon3;
    public Button CardButton1;
    public Button CardButton2;
    public Button CardButton3;

    private GameObject SelectCard;

    void Start()
    {
        CardButton1.onClick.AddListener(ViewCard1);
        CardButton2.onClick.AddListener(ViewCard2);
        CardButton3.onClick.AddListener(ViewCard3);
    }

    #region Action Subsription Managment
    public void OnEnable()
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

        SetActiveCardsOnUI();
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
            //CardViewOnUI(activecard);
            switch (activecard.CardTypeForYaricSoHeCanCalmDownAndMakeSomeUIWithoutAnyAssAcheOrSoHeCanKeepEachChairHeSitsOnByPreventingItFromFUCKINGfire)
            {
                case 1:
                    ViewCard1();
                    break;
                case 2:
                    ViewCard2();
                    break;
                case 3:
                    ViewCard3();
                    break;
                default:
                    break;
            }
        }
    }

    //TODO
    private void UnInstallCardClick(Card card)
    {
        if (MainData.ActiveWeapon.UninstallUnknownCard(card))
        {
            CardButtonUnInstall.onClick.RemoveAllListeners();
            CardNullViewOnUI(card.CardTypeForYaricSoHeCanCalmDownAndMakeSomeUIWithoutAnyAssAcheOrSoHeCanKeepEachChairHeSitsOnByPreventingItFromFUCKINGfire);
            SetActiveCardsOnUI();
        }

        if (!CardButton1.gameObject.activeInHierarchy && !CardButton2.gameObject.activeInHierarchy && !CardButton3.gameObject.activeInHierarchy)
        {
            CardButtonUnInstall.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Button Click View Cards On Weapon

    void ViewCard1()
    {
        if (MainData.ActiveWeapon.CardSlot1 != null)
            ViewCard(CardImageOnWeapon1, CardButton1, MainData.ActiveWeapon.CardSlot1);
    }
    void ViewCard2()
    {
        if (MainData.ActiveWeapon.CardSlot2 != null)
            ViewCard(CardImageOnWeapon2, CardButton2, MainData.ActiveWeapon.CardSlot2);
    }
    void ViewCard3()
    {
        if (MainData.ActiveWeapon.CardSlot3 != null)
            ViewCard(CardImageOnWeapon3, CardButton3, MainData.ActiveWeapon.CardSlot3);
    }

    void ViewCard(Image img, Button btn, Card card)
    {
        CardButtonUnInstall.gameObject.SetActive(true);
        CardButtonUnInstall.onClick.RemoveAllListeners();
        img.gameObject.SetActive(true);
        img.gameObject.transform.SetSiblingIndex(2);
        btn.gameObject.SetActive(true);

        img.sprite = card.SpriteUI;
        CardViewOnUI(card);
    }
    void SetActiveCardsOnUI()
    {
        ViewCardClear();
        ViewCard1();
        ViewCard2();
        ViewCard3();
    }
    void ViewCardClear()
    {
        CardNullViewOnUI(1);
        CardNullViewOnUI(2);
        CardNullViewOnUI(3);
    }
    #endregion

    #region HelperMethods

    private void CardViewOnUI(Card card)
    {
        if (card != null)
        {
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
            CardNullViewOnUI(null);
        }
    }

    private void CardNullViewOnUI(int? cardType)
    {
        if (cardType != null)
        {
            switch ((int)cardType)
            {
                case 1:
                    CardImageOnWeapon1.gameObject.SetActive(false);
                    CardButton1.gameObject.SetActive(false);
                    break;
                case 2:
                    CardImageOnWeapon2.gameObject.SetActive(false);
                    CardButton2.gameObject.SetActive(false);

                    break;
                case 3:
                    CardImageOnWeapon3.gameObject.SetActive(false);
                    CardButton3.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        CardNameActiveOnWeapon.text = "";
        foreach (RectTransform effect in CardEffectContentOnWeapon)
        {
            Destroy(effect.gameObject);
        }
    }

    #endregion

}
