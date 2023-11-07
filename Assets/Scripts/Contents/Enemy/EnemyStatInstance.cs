using System;
using Data.Stats;

namespace Contents.Enemy
{
    public class EnemyStatInstance
    {
        private float _damageAmount;
        private float _moveSpeed;
        private float _health;
        private float _maxHealth;
        private bool _isLive;
        public event Action<float> OnHealthChanged;
        
        public float Health
        {
            //TODO: Check if health became larger then maximum
            set
            {
                _health = value;
                OnHealthChanged?.Invoke(_health);
            }
            get => _health;
        }
        
        public float MaxHealth => _maxHealth;

        public float MoveSpeed => _moveSpeed;

        public float DamageAmount => _damageAmount;
        
        public bool IsLive => _health > 0;
        
        public EnemyStatInstance(EnemyStatData statData)
        {
            _damageAmount = statData.damageAmount;
            _moveSpeed = statData.speed;
            _maxHealth = (int) statData.maxHealth;
            _health = _maxHealth;
        }
    }
}