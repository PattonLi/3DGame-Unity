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

    public int crosshairSize = 20;//׼����������
    public float zoomSpeed = 5f;//�����л���Ұ��С���ٶ�
    public float minFOV = 20f;//��С��Ұ��
    public float maxFOV = 60f;//�����Ұ��

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
        GUI.Label(new Rect(Screen.width / 2 - 40, 60, 60, 100), "��Ϸ����!", playInfoStyle);
        
    }

    private void ShowPlayingPage()
    {
        
        GUI.Label(new Rect(10, 10, 60, 100), "������Ϸ",playInfoStyle);

        if (status.atSpot)
        {
            // Draw the message with the new GUIStyle
            GUI.Label(new Rect(10, 50, 500, 100), "���ѵ������λ,ʣ�����������" + status.shootNum, playInfoStyle);
            GUI.Label(new Rect(10, 90, 500, 100), "���ڸðе�����������" + status.spotScore, playInfoStyle);
        }
        GUI.Label(new Rect(10, 130, 500, 100), "��Ϸ�ܷ�����" + status.score, playInfoStyle);

        //show tip
        if(status.tipShowTime>0)
        {
            GUI.Label(new Rect(10, 170, 500, 100), "��ʾ��" + status.tip, playInfoStyle);
        }

        // show ׼��
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float centerX = screenWidth / 2f;
        float centerY = screenHeight / 2f;
        // ����׼����ɫ
        GUI.color = Color.red;
        // ����׼��
        GUI.DrawTexture(new Rect(centerX - crosshairSize / 2f, centerY - 90f, crosshairSize, 2f), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(centerX - 1f, centerY - crosshairSize / 2f -90f, 2f, crosshairSize), Texture2D.whiteTexture);
        // �ָ�GUI��ɫ����
        GUI.color = Color.white;

    }

}

