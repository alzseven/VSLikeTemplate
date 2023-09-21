using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Pool;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected Enemy enemy;
    private EnemyData _data;
    private ObjectPool<Enemy> _enemyPool;
    private float _spawnRange;
    private WaitForSeconds _waitForSeconds;
    private int _spawnAmount;
    private int _spawnMinimumAmount;
    private int _spawnCount;
    private float _spawnEndTime;
    private EnemySpawnData.MovePattern _movePattern;
    private SpawnPattern _spawnPattern;
    
    private IEnumerator _enumerator;
    

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
    
    public void Init(EnemySpawnData spawnData, float beginTime, float endTime)
    {
        _spawnEndTime = endTime;
        
        enemy = spawnData.enemy;
        _data = spawnData.enemyData;
        _spawnRange = spawnData.spawnRange;
        _waitForSeconds = new WaitForSeconds(spawnData.spawnDelay);
        _spawnAmount = spawnData.spawnAmount;
        _spawnMinimumAmount = spawnData.spawnMinimumAmount;
        _movePattern = spawnData.movePattern;
        _spawnPattern = spawnData.spawnPattern;
        
        if (_spawnMinimumAmount > 0)
        {
            SpawnEnemy(_spawnMinimumAmount);
        }
        _spawnCount = spawnData.spawnCount - _spawnMinimumAmount;

        
        _enumerator = _spawnCount >= 0 ? SpawnEnemyByTime(_spawnCount) : SpawnEnemyByTime();
        StartCoroutine(_enumerator);
    }
    
    public void ResetValues()
    {
        enemy = null;
        _data = null;
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
            SpawnEnemy(_spawnAmount);
            yield return _waitForSeconds;
        }
    }

    private IEnumerator SpawnEnemyByTime(int amount)
    {
        if(_spawnMinimumAmount>0) yield return _waitForSeconds;
        
        for (var i = 0; i < amount; i++)
        {
            if (GameManager.Instance.isGameEnded) break;

            SpawnEnemy(_spawnAmount);
            yield return _waitForSeconds;
        }
    }

    //TODO:
    private void SpawnEnemy(int amount)
    {
        var spPositions = _spawnPattern.GetSpawnPositions(
            amount,
            _spawnRange,
            GameManager.Instance.playerRb.position
            );
        
        foreach (var p in spPositions)
        {
            switch (_movePattern)
            {
                case EnemySpawnData.MovePattern.MOVE_FORWARD:
                    SpawnEnemy(p, GameManager.Instance.playerRb.position);
                    break;
                default:
                    SpawnEnemy(p, GameManager.Instance.playerRb);
                    break;
            }
        }
    }

    private void SpawnEnemy(Vector2 spawnPos, Rigidbody2D targetRb)
    {
        var e = _enemyPool.Get();
        e.Init(_data);
        e.targetRb = targetRb;
        e.SetPosition(spawnPos);
    }
    
    private void SpawnEnemy(Vector2 spawnPos, Vector2 targetPos)
    {
        var e = _enemyPool.Get();
        e.Init(_data);
        e.targetPos = targetPos;
        e.SetPosition(spawnPos);
    }
}

