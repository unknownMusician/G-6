using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider2D weaponCollider;
    [SerializeField]
    private List<GunModule> modules;

    public override void Attack() {
        
    }
    public override void ChangeState() {

    }
    public override Collider2D GetWeaponCollider() {
        return weaponCollider;
    }
}
