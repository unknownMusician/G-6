using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Gun : MonoBehaviour
{
    private float fireRate = 1;
    private int shotBltAmt = 1;
    private float atkArea = 1;
    public GameObject bullet;
    public List<GunModule> modules;
    public Transform firePoint;

    public float bulletSpeed;
    public float spread;

    private bool canShoot = true;
    private Timer timer;
    private void SetTimer(float time)
    {
        // Create a timer with a two second interval.
        timer = new System.Timers.Timer(time * 1000);
        // Hook up the Elapsed event for the timer. 
        timer.Elapsed += SetCanShoot;
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    private void SetCanShoot(object sender, ElapsedEventArgs e)
    {
        canShoot = true;
    }

    private void Start()
    {
        /*modules = new ArrayList
        {
            new GunModuleGen(1, 0, 1),
            new GunModuleFly(bullet)
        };*/

        foreach (GunModule mod in modules)
        {
            if (mod != null)
            {
                if (mod is GunModuleGen)
                {
                    GunModuleGen modGen = (GunModuleGen)mod;
                    this.fireRate *= modGen.GetFireRateMult();
                    this.shotBltAmt += modGen.GetShotBltAmtAdd();
                    this.atkArea *= modGen.GetAtkAreaMult();
                }
                else if (mod is GunModuleFly)
                {
                    GunModuleFly modFly = (GunModuleFly)mod;
                    this.bullet = modFly.GetBullet();
                }
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            for (int i = 0; i < shotBltAmt; i++)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation).
                    GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread)) * Vector2.right * bulletSpeed;
            }
            canShoot = false;
            SetTimer(1 / fireRate);
        }
    }
}
