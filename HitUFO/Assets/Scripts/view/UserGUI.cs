using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Status
{
    public int score;
    public string tip;
    public int roundNum;
    public int trialNum;
    public int roundIndex;
    public int trialIndex;
    public PhysiclaType type;
    public GameState gameState;
}

public class UserGUI : MonoBehaviour
{
    private Status status;
    private ISceneController currentSceneController;
    private GUIStyle playInfoStyle;




    // Start is called before the first frame update
    void Start()
    {
        Init();
        currentSceneController = Director.GetInstance().CurrentSceneController;

        // set style
        playInfoStyle = new GUIStyle();
        playInfoStyle.normal.textColor = Color.black;
        playInfoStyle.fontSize= 25;
    }

    private void Init()
    {
        status.score= 0;
        status.tip = "";
        status.roundNum = 0;
        status.trialNum = 0;
        status.gameState = GameState.Ready;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        // title
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.red;
        titleStyle.fontSize = 50;
        GUI.Label(new Rect(Screen.width / 2 - 80, 10, 60, 100), "Hit UFO", titleStyle);
        
        // show user page
        ShowPage();

    }

    public void SetIndex(int roundIndex, int  trailIndex)
    {
        status.roundIndex = roundIndex;
        status.trialIndex = trailIndex;
    }

    public void SetRTNum(int roundNum, int trailNum)
    {
        status.roundNum = roundNum;
        status.trialNum= trailNum;
    }

    public void SetGameState(GameState gameState)
    {
        status.gameState = gameState;
    }

    /*
     set property of status
     */

    public void SetScore(int score)
    {
        status.score = score;
    }

    /*
     base on game status to show different user view
     */

    private void ShowPage()
    {
        switch (status.gameState)
        {
            case GameState.Ready:
                ShowHomePage();
                break;
            case GameState.Playing:
                ShowPlayingPage();
                break;
            case GameState.GameOver:
                ShowGameoverPage();
                break;
        }
    }

    private void ShowGameoverPage()
    {
        GUI.Label(new Rect(Screen.width / 2 - 40, 60, 60, 100), "游戏结束!", playInfoStyle);
        if (GUI.Button(new Rect(420, 200, 100, 60), "重新开始"))
        {
            FirstController f = (FirstController)currentSceneController;
            // set game status
            f.Restart();
        }
    }

    private void ShowPlayingPage()
    {
        
        GUI.Label(new Rect(10, 10, 60, 100), "正在游戏",playInfoStyle);
        GUI.Label(new Rect(Screen.width  - 200, 10, 60, 100), 
            "round:" +(status.roundIndex+1)+"  trail:"+ (status.trialIndex+1), playInfoStyle);
        GUI.Label(new Rect(10, 40, 60, 100), "得分:"+status.score, playInfoStyle);
        GUI.Label(new Rect(Screen.width - 200, 35, 60, 100),
            "总轮数:" + status.roundNum + "\n每轮射击数:" + status.trialNum, playInfoStyle);
        GUI.Label(new Rect(10, 70, 60, 100),
            "当前模式："+(status.type==PhysiclaType.noPhysical?"运动学模式":"物理模式"), playInfoStyle);
        if (Input.GetButtonDown("Fire1"))
        {
            Singleton<RoundController>.Instance.Hit(Input.mousePosition);
        }

        // chose mode
        if (GUI.Button(new Rect(50, 450, 80, 50), "运动学模式"))
        {
            FirstController f = (FirstController)currentSceneController;
            f.SetGameMode(PhysiclaType.noPhysical);
        }
        if (GUI.Button(new Rect(150, 450, 80, 50), "物理学模式"))
        {
            FirstController f = (FirstController)currentSceneController;
            f.SetGameMode(PhysiclaType.pysical);
        }
    }

    private void ShowHomePage()
    {
        // add game mode
        if (GUI.Button(new Rect(Screen.width/2-50, 100, 100, 60), "开始游戏式\n(默认为运动学模式)"))
        {
            FirstController f = (FirstController)currentSceneController;
            // set game status
            f.gameState = GameState.Playing;
            status.gameState = GameState.Playing;
        }
    }

    internal void SetPhyMode(PhysiclaType phyType)
    {
        status.type= phyType;
    }
}

