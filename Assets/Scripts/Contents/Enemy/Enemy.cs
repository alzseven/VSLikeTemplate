using System;
using Data.Entity;
using UnityEngine;

namespace Contents.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStatInstance Stat;
        public Rigidbody2D targetRb;
        public Vector2 targetPos;
        
        public static event Action<Enemy> OnAnyEnemyDead;
        public event Action<Enemy> OnEnemyDead;
        
        private EnemyController _controller;
        private EnemySpriteController _spriteController;

        private void Awake()
        {
            _controller = GetComponent<EnemyController>();
            _spriteController = GetComponentInChildren<EnemySpriteController>();
        }
        
        public void Initialize(EnemyData data)
        {
            name = "Enemy " + Time.deltaTime; 
            gameObject.SetActive(true);

            Stat = new EnemyStatInstance(data.statData);
            _spriteController.Init(data.enemySprite, data.animatorController);
        }
        
        private void FixedUpdate()
        {
            // if (!Stat.IsLive) return;

            targetPos = targetRb ? targetRb.position : targetPos;
            _controller.MoveToTargetPosition(targetPos, Stat.MoveSpeed);
        }
    
        private void LateUpdate()
        {
            if (!Stat.IsLive) return;
            
            _spriteController.OnLateUpdate(_controller.IsFacingRight(targetPos));
        }
        
        public void TakeDamage(float damage)
        {
            _spriteController.OnTakeDamage(Stat.Health);
            if (Stat.Health > 0)
            {
                Stat.Health -= (int)damage;
                StartCoroutine(_controller.KnockBack());
            }
            else
            {
                _controller.StopSimulation();
                OnAnyEnemyDead?.Invoke(this);
                OnEnemyDead?.Invoke(this);
            }
        }
    }
}
