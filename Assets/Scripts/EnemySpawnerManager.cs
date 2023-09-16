using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private StageEnemyData[] stageEnemyDatas;
    [SerializeField] private EnemySpawner enemySpawner;
    private ObjectPool<EnemySpawner> _spawnerPool;

    private int _spawnCnt;
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
        _spawnCnt = 0;
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
        
        if (_spawnCnt > 0)
        {
            _spawnCnt--;
        }

        if (_spawnCnt == 0)
        {
            _currentSpawnIndex++;
            if (_currentSpawnIndex >= stageEnemyDatas.Length)
            {
                throw new NotImplementedException();
            }
            else
            {
                SetupSpawners();
            }
        }
    }
    
    private void SetupSpawners()
    {
        _spawnCnt = stageEnemyDatas[_currentSpawnIndex].enemySpawnDatas.Length;

        foreach (EnemySpawnData data in stageEnemyDatas[_currentSpawnIndex].enemySpawnDatas)
        {
            var pool = _spawnerPool.Get();
            pool.transform.parent = transform;
            pool.Init(data);
        } 
    }
}