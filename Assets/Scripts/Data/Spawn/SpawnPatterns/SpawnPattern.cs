using System.Collections.Generic;
using UnityEngine;

namespace Data.Spawn.SpawnPatterns
{
    public abstract class SpawnPattern : ScriptableObject
    {
        public abstract IEnumerable<Vector2> GetSpawnPositions(int spawnCount,
            float spawnRange,
            Vector2 centerPos);

    }
}