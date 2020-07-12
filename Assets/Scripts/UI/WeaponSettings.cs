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

    public Image MainWeaponImage;
    public Text WeaponName;
    public Text WeaponDescription;

    public List<Weapon.Info> Weapons;
    public void Awake()
    {
        MainData.ActionWeapons += SetWeapons;
        SetImageToScrollView();
    }

    private void SetWeapons()
    {
        Debug.Log("pidor");
        //Weapons = MainData.InventoryWeapons;
        SetImageToScrollView();

    }

    private void SetImageToScrollView()
    {
        foreach (RectTransform child in WeaponContent)
        {
            Destroy(child.gameObject);
        }
        //(ScrollView)WeaponsScrollView.Clear();
        foreach (Weapon.Info weapon in MainData.InventoryWeapons)
        {
            var instance = GameObject.Instantiate(WeaponButtonPrefab.gameObject) as GameObject;
            instance.transform.SetParent(WeaponContent, false);
            instance.GetComponent<Image>().sprite =
                weapon.WeaponPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
            instance.GetComponent<Button>().onClick.AddListener(delegate { WeaponClick(weapon); });
        }
    }

    private void WeaponClick(Weapon.Info activewepon)
    {
        MainWeaponImage.sprite = activewepon.WeaponPrefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        WeaponName.text = activewepon.WeaponPrefab.name;
        //WeaponDescription = activewepon.WeaponPrefab.
    }

    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
