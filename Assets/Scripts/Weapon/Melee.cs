using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{

    public override void Attack() {

    }
    //protected override void SetSprite() {

    //}
    protected override void InstallModCards() {

    }
    protected override void GetCardsFromChildren() {

    }
    public override void Reload() {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        // collision.gameObject.GetComponent<Player>().DMG();
    }
}
