using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleGen : GunModule
{
    public float fireRateMult;
    public int shotBltAmtAdd;
    public float atkAreaMult;

    public GunModuleGen(float fireRateMult, int shotBltAmtAdd, float atkAreaMult)
    {
        this.fireRateMult = fireRateMult;
        this.shotBltAmtAdd = shotBltAmtAdd;
        this.atkAreaMult = atkAreaMult;
    }

    public float GetFireRateMult()
    {
        return fireRateMult;
    }

    public int GetShotBltAmtAdd()
    {
        return shotBltAmtAdd;
    }

    public float GetAtkAreaMult()
    {
        return atkAreaMult;
    }
}
