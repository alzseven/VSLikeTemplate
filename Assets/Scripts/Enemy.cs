using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    
    private Rigidbody2D _rigidbody;
    
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    private static readonly int Hit = Animator.StringToHash("Hit");
    public RuntimeAnimatorController animatorController;
    private Animator _animator;
    
    private WaitForFixedUpdate _wait;
    
    public Transform targetTransform;
    
    private bool isLive = true;
    
    public event Action<Enemy> OnEnemyDead = delegate { };
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive) return;
        
        
        Vector2 dir = (Vector2)targetTransform.position - _rigidbody.position;
        Vector2 next = dir.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + next);
        // _rigidbody.velocity = Vector2.zero;
    }
    
    void LateUpdate()
    {
        if (!isLive) return;
        
        _renderer.flipX = targetTransform.position.x < _rigidbody.position.x;
    }
}
