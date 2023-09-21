using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    private bool _isLive = true;
    public Rigidbody2D targetRb;
    public Vector2 targetPos;
    private EnemyController _controller;
    public event Action<Enemy> OnEnemyDead = delegate { };
    private SpriteRenderer _renderer;
    private Animator _animator;
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<EnemyController>();
    }

    //TODO:
    public void Init(EnemyData data)
    {
        name = "Enemy " + Time.deltaTime; 
        gameObject.SetActive(true);
        maxHealth = data.statData.maxHealth;
        speed = data.statData.speed;
        health = maxHealth;
        _isLive = true;
        _renderer.sprite = data.enemySprite;
        _animator.runtimeAnimatorController = data.animatorController;
    }

    public void SetPosition(Vector2 dest)
    {
        transform.position = dest;
    }

    private void FixedUpdate()
    {
        if (!_isLive) return;

        targetPos = targetRb ? targetRb.position : targetPos;
        _controller.MoveToTargetPosition(targetPos, speed);
    }
    
    private void LateUpdate()
    {
        if (!_isLive) return;
        _renderer.flipX = _controller.IsFacingRight(targetPos);
    }

    //TODO:
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(_controller.KnockBack());        

        if (health > 0)
        {
            _animator.SetTrigger(Hit);
        }
        else
        {
            _isLive = false;
            _animator.SetBool(Dead, true);
            Invoke(nameof(OnDie), 1f);
        }
    }

    //TODO:
    private void OnDie()
    {
        OnEnemyDead(this);
    }
}
