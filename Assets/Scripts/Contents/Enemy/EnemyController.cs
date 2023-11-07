using System.Collections;
using Contents.Manager;
using UnityEngine;

namespace Contents.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private WaitForFixedUpdate _wait;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _wait = new WaitForFixedUpdate();
        }

        public void MoveToTargetPosition(Vector2 targetPos, float moveSpeed)
        {
            var curPos = _rigidbody.position;

            Vector2 dir = targetPos - curPos;
            Vector2 dest = dir.normalized * (moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(curPos + dest);
        }

        public IEnumerator KnockBack()
        {
            var playerPos = GameManager.Instance.playerRb.position;
            var dir = _rigidbody.position - playerPos;
            _rigidbody.AddForce(dir.normalized, ForceMode2D.Impulse);
            yield return _wait;
            //TODO:
            _rigidbody.velocity = Vector2.zero;
        }

        public bool IsFacingRight(Vector2 targetPos)
        {
            return targetPos.x - _rigidbody.position.x < 0;
        }

        public void StopSimulation()
        {
            _rigidbody.simulated = false;
        }
    }
}