using Data.Stats;
using UnityEngine;

namespace Contents.Weapon
{
    public abstract class ProjectileWeapon : MonoBehaviour
    {
        public GameObject projectile;
        public float damage;
        public int amount;
        public float coolDown;
        public float areaSize;
        public float duration;
        public float speed;
        public int pierce;
        public WeaponStats weaponStats;
        
        public virtual void Initialize(WeaponStats stats)
        {
            weaponStats = stats;
            
            
            damage = stats.weaponDamage;
            amount = stats.weaponAmount;
            coolDown = stats.weaponCoolDown;
            areaSize = stats.weaponAreaSize;
            duration = stats.weaponDuration;
            speed = stats.weaponSpeed;
            pierce = stats.weaponPierce;
        }


        public virtual void LevelUp(WeaponStats stats)
        {
            weaponStats = stats;
            
            
            damage = stats.weaponDamage;
            amount = stats.weaponAmount;
            coolDown = stats.weaponCoolDown;
            areaSize = stats.weaponAreaSize;
            duration = stats.weaponDuration;
            speed = stats.weaponSpeed;
            pierce = stats.weaponPierce;
        }
    }
}
