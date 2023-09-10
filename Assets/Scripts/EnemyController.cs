using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    private Rigidbody2D _rigidbody;
    private bool isLive = true;
    private SpriteRenderer _renderer;
    private EnemyStats _stats;
    
    private void Awake()
    {
        _stats = GetComponent<EnemyStats>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive) return;
        
        
        Vector2 dir = target.position - _rigidbody.position;
        Vector2 next = dir.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + next);
        _rigidbody.velocity = Vector2.zero;
    }
    
    void LateUpdate()
    {
        if (!isLive) return;
        
        _renderer.flipX = target.position.x < _rigidbody.position.x;
    }
}
