using UnityEngine;

namespace Contents.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSpriteController : MonoBehaviour
    {
        
        private PlayerMovement _playerMovement;
        private SpriteRenderer _renderer;
        private Animator _animator;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }

        private void LateUpdate()
        {
            _renderer.flipX = _playerMovement.InputVec.x < 0;
        
            _animator.SetFloat(MoveSpeed, _playerMovement.InputVec.magnitude);
        }
    }
}
