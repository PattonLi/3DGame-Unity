using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum GameStatus
{
    Playing,
    GameOver,
}

struct Status
{
    public int score;
    public string tip;
    public float tipShowTime;
    public bool atSpot;
    public int shootNum;
    public GameStatus gameStatus;
    public int spotScore;
}

public class UserGUI : MonoBehaviour
{
    private ISceneController currentSceneController;
    private GUIStyle playInfoStyle;
    private Status status;

    public int crosshairSize = 20;//准星线条长度
    public float zoomSpeed = 5f;//滚轮切换视野大小的速度
    public float minFOV = 20f;//最小视野宽
    public float maxFOV = 60f;//最大视野宽

    // Start is called before the first frame update
    void Start()
    {
        Init();
        currentSceneController = Director.GetInstance().CurrentSceneController;

        // set style
        playInfoStyle = new GUIStyle();
        playInfoStyle.normal.textColor = Color.black;
        playInfoStyle.fontSize= 25;

        // init status
        status.shootNum = 0;
        status.score = 0;
        status.tip = string.Empty;
        status.atSpot = false;
        status.gameStatus = GameStatus.Playing;
        status.spotScore = 0;
        status.tipShowTime = 0;
    }

    private void Init()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        status.tipShowTime -= Time.deltaTime;
    }

    private void OnGUI()
    {
        // show user page
        ShowPage();
    }

    public void SetGameState(GameStatus gameStatus)
    {
        status.gameStatus = gameStatus;
    }

    /*
     set property of status
     */

    public void SetScore(int score)
    {
        status.score = score;
    }

    public void SetSpotScore(int score)
    {
        status.spotScore = score;
    }

    public void AddScore(int score)
    {
        status.score+=score;
    }

    public void SetShootNum(int shootNum)
    {
        status.shootNum = shootNum;
    }
    
    public void SetIsAtSpot(bool isAtSpot)
    {
        status.atSpot = isAtSpot;
    }

    public void SetTip(string tip,float time)
    {
        status.tip = tip;
        status.tipShowTime=time;
    }

    /*
     base on game status to show different user view
     */

    private void ShowPage()
    {
        switch (status.gameStatus)
        {
            case GameStatus.Playing:
                ShowPlayingPage();
                break;
            case GameStatus.GameOver:
                ShowGameoverPage();
                break;
        }
    }

    private void ShowGameoverPage()
    {
        GUI.Label(new Rect(Screen.width / 2 - 40, 60, 60, 100), "游戏结束!", playInfoStyle);
        
    }

    private void ShowPlayingPage()
    {
        
        GUI.Label(new Rect(10, 10, 60, 100), "正在游戏",playInfoStyle);

        if (status.atSpot)
        {
            // Draw the message with the new GUIStyle
            GUI.Label(new Rect(10, 50, 500, 100), "您已到达射击位,剩余射击次数：" + status.shootNum, playInfoStyle);
            GUI.Label(new Rect(10, 90, 500, 100), "您在该靶点的射击分数：" + status.spotScore, playInfoStyle);
        }
        GUI.Label(new Rect(10, 130, 500, 100), "游戏总分数：" + status.score, playInfoStyle);

        //show tip
        if(status.tipShowTime>0)
        {
            GUI.Label(new Rect(10, 170, 500, 100), "提示：" + status.tip, playInfoStyle);
        }

        // show 准星
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float centerX = screenWidth / 2f;
        float centerY = screenHeight / 2f;
        // 设置准星颜色
        GUI.color = Color.red;
        // 绘制准星
        GUI.DrawTexture(new Rect(centerX - crosshairSize / 2f, centerY - 90f, crosshairSize, 2f), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(centerX - 1f, centerY - crosshairSize / 2f -90f, 2f, crosshairSize), Texture2D.whiteTexture);
        // 恢复GUI颜色设置
        GUI.color = Color.white;

    }

}

