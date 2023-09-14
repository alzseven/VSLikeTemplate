using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemyControllers;
    private ObjectPool<EnemyController> _enemyPool;
    private Transform[] _spawnPoint;
    private int _level;
    private float _timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
        _enemyPool = new ObjectPool<EnemyController>(CreateObject,
            OnTakeObjectFromPool,
            OnReturnObjectToPool);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    #region EnemyPool

    private EnemyController CreateObject()
    {
        // TODO: Spawn different enemy by game time OR something else...
        var ec = Instantiate(enemyControllers[0]);
 
        return ec;
    }
    
    private void OnTakeObjectFromPool(EnemyController ec)
    {
        ec.gameObject.SetActive(true);
        // TODO:
        ec.target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        ec.OnEnemyDead += ReleaseObj;

    }
    
    private void OnReturnObjectToPool(EnemyController ec)
    {
        ec.OnEnemyDead -= ReleaseObj;
        ec.gameObject.SetActive(false);
    }

    private void ReleaseObj(EnemyController ec)
    {
        _enemyPool.Release(ec);
    }


    #endregion

    private IEnumerator SpawnEnemy()
    {
        // TODO: Til game ends...
        while (_timer < 10.0f)
        {
            var enemy = _enemyPool.Get();
            enemy.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length-1)].position;
            // TODO: cache OR get time from GM or somewhere else...
            yield return new WaitForSeconds(2f);
        }
    }
}

