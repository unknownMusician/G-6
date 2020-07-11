using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class WeaponSettings : MonoBehaviour
{
    public ScrollView WeaponsScrollView;
    public List<Weapon.Info> Weapons;
    public void Awake()
    {
        MainData.ActionWeapons += SetWeapons;
    }

    private void SetWeapons()
    {
        Weapons = MainData.InventoryWeapons;
    }

    private void SetImageToScrollView()
    {
        WeaponsScrollView.Clear();
        foreach (Weapon.Info weapon in Weapons)
        {
            
            Image im = null;
            im.sprite = weapon.WeaponPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
 
            //WeaponsScrollView.contentViewport.contentContainer.Add();
        }
    }

    public void Exit()
    {
        //PauseMenu.Resume();
    }
}
