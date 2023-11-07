using Contents.Manager;
using Data.Stats;
using UnityEngine;

namespace Contents.Weapon
{
    public class SpinningShovel : ProjectileWeapon
    {
        public override void Initialize(WeaponStats stats)
        {
            weaponStats = stats;
            
            //TODO: Replace with base.Initialize after testing
            damage = stats.weaponDamage;
            amount = stats.weaponAmount * 2;
            coolDown = 300 / stats.weaponCoolDown;
            areaSize = stats.weaponAreaSize;
            duration = stats.weaponDuration;
            speed = stats.weaponSpeed;
            pierce = stats.weaponPierce;
            
            Batch();
        }
        
        void Update()
        {
            if (GameManager.Instance.isGameEnded) return;
            
            transform.Rotate(Vector3.back * (coolDown * Time.deltaTime));
        }

        public override void LevelUp(WeaponStats stats)
        {
            weaponStats = stats;
            
            //TODO: Replace with base.Initialize after testing
            damage = stats.weaponDamage;
            amount = stats.weaponAmount * 2;
            coolDown = 300 / stats.weaponCoolDown;
            areaSize = stats.weaponAreaSize;
            duration = stats.weaponDuration;
            speed = stats.weaponSpeed;
            pierce = stats.weaponPierce;
            
            Batch();
        }

        private void Batch()
        {
            for (int i = 0; i < amount; i++)
            {
                Transform pjTransform;

                if (i < transform.childCount)
                {
                    pjTransform = transform.GetChild(i);
                }
                else
                {
                    //TODO: Get pj from pool
                    pjTransform = Instantiate(projectile).transform;
                    pjTransform.parent = transform;
                }

                pjTransform.localPosition = Vector3.zero;
                pjTransform.localRotation = Quaternion.identity;

                Vector3 rotVec = Vector3.forward * 360 * i / amount;
                pjTransform.Rotate(rotVec);
                pjTransform.Translate(pjTransform.up * 1.5f, Space.World);
                pjTransform.GetComponent<WeaponProjectile>().Initialize(damage);
            }
        }
    }
}
