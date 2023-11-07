using UnityEngine;

namespace Contents.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponProjectile : MonoBehaviour
    {
        public float damage;
        // projectile speed
        [SerializeField] private float speed;

        private int _pierce;
        private float _duration;
        private Vector2 _dir;
        private Rigidbody2D _rigidbody2D;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void Initialize(float dmg) => damage = dmg;

        public void Initialize(float dmg, Vector3 dir)
        {
            damage = dmg;
            _rigidbody2D.velocity = dir * speed;
            _dir = dir;
        }
        
        private void FixedUpdate()
        {
            if (_dir != Vector2.zero)
            {
                _rigidbody2D.MovePosition(_rigidbody2D.position + _dir * (speed * Time.fixedDeltaTime));
            }
        }

        //TODO: Case of pierce
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (other.TryGetComponent<Enemy.Enemy>(out var e)) e.TakeDamage(damage);
            }
        }
    }
}
