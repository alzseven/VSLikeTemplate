using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StageEnemyData", menuName = "Data/StageEnemyData", order = 0)]
public class StageEnemyData : ScriptableObject
{
    public EnemySpawnData[] enemySpawnDatas;
}

[Serializable]
public class EnemySpawnData
{
    public EnemyController enemyController;
    public float spawnRange;
    public float spawnDelay;
    public float spawnEndTime;
    public int spawnAmount;
    public int spawnMinimumAmount;
}