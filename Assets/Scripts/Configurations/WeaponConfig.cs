using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig : BaseConfig<WeaponConfigEntity> {
    public struct Gun {
        public struct Pistol {
            int clipSize;
            int pocketSize;
            float standardBulletSpeed;
            float standardBulletLifetime;
            float standardBulletDamage;
            float standardMeleeDamage;
            float standardMaxThrowDamage;
            float standardReloadTime;
            float standardRecoilTime;
            float standardThrowLoadTime;
        }
    }

    public struct Melee {
        public struct Sword {
            float standardMeleeDamage;
            float standardMaxThrowDamage;
            float standardRecoilTime;
            float standardThrowLoadTime;
        }
    }

    public WeaponConfig(string name) : base(name) {

    }
}
