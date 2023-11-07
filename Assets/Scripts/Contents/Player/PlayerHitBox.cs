using Data.Variable;
using UnityEngine;

namespace Contents.Player
{
    public class PlayerHitBox : MonoBehaviour
    {
        public StatFloatWithMax playerHealth;
        
        private void OnCollisionStay2D(Collision2D other)
        {
            var enemy = other.collider.GetComponentInParent<Enemy.Enemy>();
            if (enemy != null)
            {
                var stats = enemy.Stat;
                playerHealth.Value -= (stats.DamageAmount * Time.deltaTime);
            }
        }
    }
}