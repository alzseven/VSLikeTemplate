using Contents.Spawner;
using Data;
using Data.Spawn;
using UnityEngine;

namespace Contents.Manager
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private StageEnemyData[] stageEnemyData;
        [SerializeField] private EnemySpawner enemySpawner;
        
        private int _activeSpawnerCount;
        private int _currentSpawnIndex;
        private StageEnemyData _currentSpawnData;
        
        private void Awake()
        {
            _activeSpawnerCount = 0;
            _currentSpawnIndex = 0;
        }

        private void Start()
        {
            SetupSpawners();
        }
        
        private void OnSpawnEnded(EnemySpawner spawner)
        {
            spawner.OnSpawnEnded -= OnSpawnEnded;
            Destroy(spawner.gameObject);
            
            if (_activeSpawnerCount > 0)
            {
                _activeSpawnerCount--;
            }
        
            if (_activeSpawnerCount == 0)
            {
                _currentSpawnIndex++;
                if (_currentSpawnIndex >= stageEnemyData.Length)
                {
                    // TODO: No more wave! DO Something!
                }
                else
                {
                    SetupSpawners();
                }
            }
        }
        
        private void SetupSpawners()
        {
            _currentSpawnData = stageEnemyData[_currentSpawnIndex];
        
            _activeSpawnerCount = _currentSpawnData.enemySpawnData.Length;

            foreach (EnemySpawnData data in _currentSpawnData.enemySpawnData)
            {
                //TODO: Instantiate spawners at the beginning?
                var pool = Instantiate(enemySpawner, transform);
                if (pool.TryGetComponent<EnemySpawner>(out var spawner)) spawner.OnSpawnEnded += OnSpawnEnded;
                pool.Init(data, _currentSpawnData.spawnBeginTime, _currentSpawnData.spawnEndTime);
            } 
        }
    }
}