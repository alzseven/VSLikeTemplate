using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private ObjectPool<Enemy> _enemyPool;
    private float _spawnEndTime;
    private WaitForSeconds _waitForSeconds;
    private int _spawnAmount;
    private IEnumerator _enumerator;
    private float _spawnRange;
    private int _spawnMinimumAmount;
    public event Action<EnemySpawner> OnSpawnEnded = delegate { };
    
    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateObject,
            OnTakeObjectFromPool,
            OnReturnObjectToPool,
            OnDestroyPoolObject,
            true,
            50,
            100
        );
    }

    public void Init(EnemySpawnData spawnData)
    {
        enemy = spawnData.enemy;
        _spawnEndTime = spawnData.spawnEndTime;
        _waitForSeconds = new WaitForSeconds(spawnData.spawnDelay);
        _spawnRange = spawnData.spawnRange;
        _spawnMinimumAmount = spawnData.spawnMinimumAmount;
        if (_spawnMinimumAmount > 0)
        {
            for (var i = 0; i < _spawnMinimumAmount; i++)
            {
                SpawnEnemy();
            }
        }
        _spawnAmount = spawnData.spawnAmount - _spawnMinimumAmount;
        _enumerator = _spawnAmount > 0 ? SpawnEnemyByTime(_spawnAmount) : SpawnEnemyByTime();
        StartCoroutine(_enumerator);
    }

    public void ResetValues()
    {
        enemy = null;
        _spawnEndTime = 99999;
        _waitForSeconds = null;
        _spawnAmount = 0;
        _enumerator = null;
        _spawnRange = 0;
        _spawnMinimumAmount = 0;
    }
    
    #region EnemyPool

    private Enemy CreateObject()
    {
        var ec = Instantiate(enemy);
        return ec;
    }
    
    private void OnTakeObjectFromPool(Enemy e)
    {
        e.gameObject.SetActive(true);
        e.OnEnemyDead += ReleaseObj;

    }
    
    private void OnReturnObjectToPool(Enemy e)
    {
        e.OnEnemyDead -= ReleaseObj;
        OnSpawnEnded(this);
        e.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Enemy e)
    {
        Destroy(e.gameObject);
    }
    

    #endregion

    private void ReleaseObj(Enemy e)
    {
        _enemyPool.Release(e);
    }
    
    private void Update()
    {
        if (GameManager.Instance.CurrentGameTime >= _spawnEndTime)
        {
            StopCoroutine(_enumerator);
            OnSpawnEnded(this);
        }
    }

    private IEnumerator SpawnEnemyByTime()
    {
        if(_spawnMinimumAmount>0) yield return _waitForSeconds;
        while (!GameManager.Instance.isGameEnded)
        {
            SpawnEnemy();
            yield return _waitForSeconds;
        }
        OnSpawnEnded(this);
    }
    private IEnumerator SpawnEnemyByTime(int amount)
    {
        if(_spawnMinimumAmount>0) yield return _waitForSeconds;
        for (var i = 0; i < amount; i++)
        {
            if (GameManager.Instance.isGameEnded) break;

            SpawnEnemy();
            yield return _waitForSeconds;
        }
        OnSpawnEnded(this);
    }

    private void SpawnEnemy()
    {
        var enemy = _enemyPool.Get();
        enemy.targetTransform = GameManager.Instance.playerTransform;
        enemy.transform.position = GameManager.Instance.playerTransform.position +
                                   (Vector3) Random.insideUnitCircle.normalized * _spawnRange;
    }
}

