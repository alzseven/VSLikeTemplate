using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemyStatData", menuName = "Data/EnemyStatData", order = 1)]
    public class EnemyStatData : ScriptableObject
    {
        public float speed;
        public float health;
        public float maxHealth;
    
    
    }
}

