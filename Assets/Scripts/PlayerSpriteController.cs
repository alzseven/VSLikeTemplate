using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteController : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        _renderer.flipX = _playerMovement.InputVec.x < 0;
        
        _animator.SetFloat("MoveSpeed", _playerMovement.InputVec.magnitude);
    }
}
