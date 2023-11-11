using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 游戏状态，0为准备进行，1为正在进行游戏，2为结束 */
public enum GameState
{
    Ready = 0, Playing = 1, GameOver = 2
};



public class FirstController : MonoBehaviour, ISceneController
{
    
    public DiskActionManager actionManager;
    public RoundController roundController;
    public UserGUI userGUI;

    public GameState gameState;

    // ruler config
    public Ruler rulerConfig;

     


    public void LoadResources()
    {
        Director.GetInstance().CurrentSceneController = this;
        
        roundController = gameObject.AddComponent<RoundController>();
        Debug.Log("add roundController Component");

        userGUI = gameObject.AddComponent<UserGUI>();
        Debug.Log("add UserGUI");

        actionManager = gameObject.AddComponent<DiskActionManager>();
        Debug.Log("add DiskActionManager");

        gameState = (int)GameState.Ready;
    }

    
    public void Restart()
    {
        Director.ReloadCurrentScene();
    }


    public void JudgeResultCallBack(string result)
    {

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
        gameState = GameState.GameOver;
        userGUI.SetGameState(GameState.GameOver);
    }

    public void SetGameMode(PhysiclaType type)
    {
        roundController.phyType= type;
    }
}