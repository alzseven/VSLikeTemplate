using Contents.Manager;
using UnityEngine;

namespace Contents.Weapon
{
    public class Riffle : ProjectileWeapon
    {
        public float scanRange;
        public LayerMask targetLayer;
        public Transform nearestTarget;
        
        private RaycastHit2D[] _targets;
        private float _timer;

        private void FixedUpdate()
        { 
            //TODO:
            _targets = Physics2D.CircleCastAll(transform.position,
                scanRange,
                Vector2.zero,
                0,
                targetLayer);
            nearestTarget = GetNearest();
        }

        private Transform GetNearest()
        {
            Transform result = null;
            float diff = float.MaxValue;
        
            foreach (RaycastHit2D target in _targets)
            {
                Vector3 mypos = transform.position;
                Vector3 targetPos = target.transform.position;
                float curDiff = Vector3.Distance(mypos, targetPos);
                if (curDiff < diff)
                {
                    diff = curDiff;
                    result = target.transform;
                }
            }
            return result;
        }
        

        private void Fire()
        {
            if (!nearestTarget) return;
            
            // TODO: Different by weapon
            Vector3 targetPos = nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            // TODO: Get bullet from pool
            Transform bullet = Instantiate(projectile).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<WeaponProjectile>().Initialize(damage, dir);
        }
        
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.isGameEnded) return;
            
            //TODO:
            _timer += Time.deltaTime;

            if (_timer > coolDown)
            {
                _timer -= coolDown;
                Fire();
            }
        }
    }
}