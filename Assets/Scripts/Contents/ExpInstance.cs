using Contents.Player;
using Data.Variable;
using UnityEngine;

namespace Contents
{
    public class ExpInstance : MonoBehaviour
    {
        public float expAmount;
        public StatFloatWithMax exp;
        [SerializeField] private Rigidbody2D targetRigidbody;
        [SerializeField] private float moveDuration = 1;
        
        private float _movedTime;
        private float _timeElapsed;
        
        void Update()
        {
            if (targetRigidbody == null) return;

            if (_timeElapsed < moveDuration)
            {
                //TODO:
                transform.position = new Vector3(
                    EaseInQuint(transform.position.x, targetRigidbody.position.x, _timeElapsed / moveDuration),
                    EaseInQuint(transform.position.y, targetRigidbody.position.y, _timeElapsed / moveDuration),
                    0f);
                _timeElapsed += Time.deltaTime;
            }
            else 
            {
                transform.position = targetRigidbody.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO:
            if (other.TryGetComponent<PlayerStats>(out var stats))
            {
                //TODO: Should be method
                exp.Value += expAmount;
                Destroy(gameObject);
            }
        }

        private static float EaseInQuint(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value * value + start;
        }
        
        public void SetTarget(Rigidbody2D targetRigidbody)
        {
            this.targetRigidbody = targetRigidbody;
        }
    }
}
