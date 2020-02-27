using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleFly : GunModule
{
    public GameObject bullet;

    public GunModuleFly(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public GameObject GetBullet()
    {
        return bullet;
    }
}
