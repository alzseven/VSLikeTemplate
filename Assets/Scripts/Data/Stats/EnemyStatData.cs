using UnityEngine;

namespace Data.Stats
{
    [CreateAssetMenu(fileName = "EnemyStatData", menuName = "Data/Stats/Enemy", order = 1)]
    public class EnemyStatData : ScriptableObject
    {
        public int damageAmount;
        public float speed;
        public float health;
        public float maxHealth;
    }
}

