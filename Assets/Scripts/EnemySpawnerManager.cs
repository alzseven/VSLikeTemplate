using System;
using Data;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private StageEnemyData[] stageEnemyData;
    [SerializeField] private EnemySpawner enemySpawner;
    private ObjectPool<EnemySpawner> _spawnerPool;

    private int _activeSpawnerCount;
    private int _currentSpawnIndex;
    
    private void Awake()
    {
        _spawnerPool = new ObjectPool<EnemySpawner>(CreateObject,
            OnTakeObjectFromPool,
            OnReturnObjectToPool,
            OnDestroyPoolObject,
            true,
            5,
            10
            );
        _activeSpawnerCount = 0;
        _currentSpawnIndex = 0;
    }

    private void Start()
    {
        SetupSpawners();
    }


    #region EnemySpawnerPool

    private EnemySpawner CreateObject()
    {
        var ec = Instantiate(enemySpawner);
        ec.ResetValues();
        return ec;
    }
    
    private void OnTakeObjectFromPool(EnemySpawner spawner)
    {
        spawner.OnSpawnEnded += OnSpawnEnded;
        spawner.gameObject.SetActive(true);
    }
    
    private void OnReturnObjectToPool(EnemySpawner spawner)
    {
        spawner.OnSpawnEnded -= OnSpawnEnded;
        spawner.ResetValues();
        spawner.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EnemySpawner spawner)
    {
        Destroy(spawner.gameObject);
    }
    
    #endregion

    private void OnSpawnEnded(EnemySpawner spawner)
    {
        _spawnerPool.Release(spawner);
        
        if (_activeSpawnerCount > 0)
        {
            _activeSpawnerCount--;
        }

        if (_activeSpawnerCount == 0)
        {
            _currentSpawnIndex++;
            if (_currentSpawnIndex >= stageEnemyData.Length)
            {
                // throw new NotImplementedException();
            }
            else
            {
                SetupSpawners();
            }
        }
    }
    
    private void SetupSpawners()
    {
        var currentSpawnData = stageEnemyData[_currentSpawnIndex];
        
        _activeSpawnerCount = currentSpawnData.enemySpawnData.Length;

        foreach (EnemySpawnData data in currentSpawnData.enemySpawnData)
        {
            var pool = _spawnerPool.Get();
            pool.transform.parent = transform;
            pool.Init(data, currentSpawnData.spawnBeginTime, currentSpawnData.spawnEndTime);
        } 
    }
}