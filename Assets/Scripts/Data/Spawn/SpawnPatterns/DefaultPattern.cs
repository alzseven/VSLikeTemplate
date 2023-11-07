using System.Collections.Generic;
using UnityEngine;

namespace Data.Spawn.SpawnPatterns
{
    [CreateAssetMenu(fileName = "DefaultPattern", menuName = "Data/Spawn/SpawnPatterns/Default", order = 0)]
    public class DefaultPattern : SpawnPattern
    {
        public override IEnumerable<Vector2> GetSpawnPositions(int spawnCount,
            float spawnRange,
            Vector2 centerPos)
        {
            var res = new Vector2[spawnCount];
            for (int i = 0; i < spawnCount; i++)
            {
                res[i] = centerPos
                         + Random.insideUnitCircle.normalized * spawnRange;
            }
            return res;
        }
    }
}