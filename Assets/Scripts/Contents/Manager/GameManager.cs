using System;
using Core;
using Data.Variable;
using UnityEngine;

namespace Contents.Manager
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        //TODO:
        public Transform playerTransform;
        public Rigidbody2D playerRb;

        public StatFloatWithMax currentGameTime;
        public StatFloatWithMax killCount;
        
        //TODO: Move to PlayerStats?
        public StatFloatWithMax playerLevel;
        public StatFloatWithMax exp;
        
        public GameObject expInstance;
        public bool isGameEnded;
        public event Action<int> OnLevelUp;

        private void Start()
        {
            isGameEnded = false;
            //TODO:
            currentGameTime.Value = 0;
            killCount.Value = 0;
            playerLevel.Value = 1;
            exp.Value = 0;
        }

        private void Update()
        {
            currentGameTime.Value += Time.deltaTime;
        }

        private void OnEnable()
        {
            Enemy.Enemy.OnAnyEnemyDead += CreateExpInstance;
            exp.OnValueChanged += OnExpChanged;
        }

        private void OnDisable()
        {
            Enemy.Enemy.OnAnyEnemyDead -= CreateExpInstance;
            exp.OnValueChanged -= OnExpChanged;
        }

        //TODO: Exp Manager?
        //TODO: Separate from GameManager
        private void CreateExpInstance(Enemy.Enemy enemy)
        {
            killCount.Value++;
            Instantiate(expInstance, enemy.transform.position, Quaternion.identity);
        }

        private void OnExpChanged(float expAdded)
        {
            if (exp.Value >= exp.GetMaxValue)
            {
                playerLevel.Value++;
                OnLevelUp?.Invoke((int) playerLevel.Value);
                Time.timeScale = 0f;
            }
        }

        public void OnUpgradeSelected()
        {
            Time.timeScale = 1f;
            exp.Value -= exp.GetMaxValue;
        }
    }
}