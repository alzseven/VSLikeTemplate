using System;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public Transform playerTransform;
    public float CurrentGameTime { get; private set; }
    public bool isGameEnded;

    private void Start()
    {
        isGameEnded = false;
    }

    private void Update()
    {
        CurrentGameTime += Time.deltaTime;
        
        
    }



}