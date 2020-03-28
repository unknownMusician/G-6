using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProps {
    public int Enemy { get; set; }
    public int Magnetting { get; set; }
    public bool Ricochet { get; set; }
    public bool Piercing { get; set; }
    public bool Homing { get; set; }
    public bool Teleporting { get; set; }
    public bool Magnet { get; set; }

    public BulletProps(
        bool ricochet = false, 
        bool piercing = false, 
        bool homing = false, 
        bool teleporting = false, 
        bool magnet = false,
        int enemy = 0,
        int magnetting = 0) {
        
        this.Ricochet = ricochet;
        this.Piercing = piercing;
        this.Homing = homing;
        this.Teleporting = teleporting;
        this.Magnet = magnet;
        this.Enemy = enemy;
        this.Magnetting = magnetting;
    }
}
