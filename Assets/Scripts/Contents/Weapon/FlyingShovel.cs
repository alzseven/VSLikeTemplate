using System.Collections;
using Contents.Manager;
using Contents.Player;
using Data.Stats;
using UnityEngine;

namespace Contents.Weapon
{
    public class FlyingShovel : ProjectileWeapon
    {
        private Vector2 _dir = Vector2.right;
        private PlayerMovement _playerMovement;
        
        //TODO: cooldown change by levelup OR other powerups...
        private WaitForSeconds _waitForSeconds;
        
        public override void Initialize(WeaponStats stats)
        {
            base.Initialize(stats);
            
            _waitForSeconds = new WaitForSeconds(coolDown);
            _playerMovement = GameManager.Instance.playerRb.GetComponent<PlayerMovement>();
            StartCoroutine(ShootProjectile());
        }

        private void Fire()
        {
            Vector3 dir = _dir;
            dir = dir.normalized;

            // TODO: Get bullet from pool
            Transform bullet = Instantiate(projectile).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<WeaponProjectile>().Initialize(damage, dir);
        }
        
        private IEnumerator ShootProjectile()
        {
            while (!GameManager.Instance.isGameEnded)
            {
                Fire();
                yield return _waitForSeconds;
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.isGameEnded) return;

            _dir = _playerMovement.InputVec != Vector2.zero
                ? _playerMovement.InputVec
                : _dir;
        }
    }
}