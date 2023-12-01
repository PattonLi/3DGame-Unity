using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstController : MonoBehaviour, ISceneController
{
    
    public UserGUI userGUI;
    public GameStatus gameStatus;
    public ArrowActionManager arrowActionManager;
    
    public void LoadResources()
    {
        Director.GetInstance().CurrentSceneController = this;
        
        userGUI = gameObject.AddComponent<UserGUI>();
        Debug.Log("add UserGUI");

        arrowActionManager = gameObject.AddComponent<ArrowActionManager>();
        Debug.Log("add DiskActionManager");

        gameStatus = GameStatus.Playing;
    }


    void Awake()
    {
        LoadResources();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void GameOver()
    {
        gameStatus = GameStatus.GameOver;
        userGUI.SetGameState(GameStatus.GameOver);
    }
}