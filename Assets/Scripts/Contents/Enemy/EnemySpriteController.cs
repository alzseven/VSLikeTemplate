using UnityEngine;

namespace Contents.Enemy
{
    public class EnemySpriteController : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Animator _animator;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void Init(Sprite enemySprite, RuntimeAnimatorController animatorController )
        {
            _renderer.sprite = enemySprite;
            _animator.runtimeAnimatorController = animatorController;
        }
        
        public void OnLateUpdate(bool isFacingRight)
        {
            _renderer.flipX = isFacingRight;
        }
        
        public void OnTakeDamage(float health)
        {
            if (health > 0)
            {
                _animator.SetTrigger(Hit);
            }
            else
            {
                _animator.SetBool(Dead, true);
            }
        }
    }
}