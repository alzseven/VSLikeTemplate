using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "StageEnemyData", menuName = "Data/StageEnemyData", order = 0)]
    public class StageEnemyData : ScriptableObject
    {
        public float spawnBeginTime;
        public float spawnEndTime;
        public EnemySpawnData[] enemySpawnData;
    }
}