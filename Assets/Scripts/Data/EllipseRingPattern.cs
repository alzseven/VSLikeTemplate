using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EllipseRingPattern", menuName = "data/pattern/EllipseRing", order = 1)]
    public class EllipseRingPattern : SpawnPattern
    {
        public override IEnumerable<Vector2> GetSpawnPositions(int spawnCount,
            float spawnRange,
            Vector2 centerPos)
        {
            var res = new Vector2[spawnCount];

            for (var pointNum = 0; pointNum < spawnCount; pointNum++)
            {

                // "i" now represents the progress around the circle from 0-1
                // we multiply by 1.0 to ensure we get a fraction as a result.
                var i = (pointNum * 1.0f) / spawnCount;

                // get the angle for this step (in radians, not degrees)
                var angle = i * Mathf.PI * 2;

                // the X & Y position for this angle are calculated using Sin & Cos
                var x = Mathf.Sin(angle) * spawnRange * 1.2f;
                var y = Mathf.Cos(angle) * spawnRange;

                var pos = new Vector2(x, y) + centerPos;

                res[pointNum] = pos;
                // no need to assign the instance to a variable unless you're using it afterwards:
                // Instantiate (beadPrefab, pos, Quaternion.identity);

            }
            return res;
        }
    }
}