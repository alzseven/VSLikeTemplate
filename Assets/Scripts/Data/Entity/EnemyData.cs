using Data.Stats;
using UnityEngine;

namespace Data.Entity
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 3)]
    public class EnemyData : ScriptableObject
    {
        public Sprite enemySprite;
        public RuntimeAnimatorController animatorController;
        public EnemyStatData statData;
    }
}