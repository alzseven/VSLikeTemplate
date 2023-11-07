using Contents.Enemy;
using Data.Entity;
using Data.Spawn.SpawnPatterns;
using UnityEngine;

namespace Data.Spawn
{
    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Data/Spawn/EnemySpawnData", order = 0)]
    public class EnemySpawnData : ScriptableObject
    {
        //TODO:
        public enum MovePattern
        {
            MOVE_TO_PLAYER,
            MOVE_FORWARD,
        }
        
        public Enemy enemy;
        public EnemyData enemyData;
        public float spawnRange;
        public float spawnDelay;
        public int spawnAmount;
        public int spawnMinimumAmount;
        public int spawnCount;  //TODO: Max(1, value)?
        public MovePattern movePattern;
        public SpawnPattern spawnPattern;
    }
}
