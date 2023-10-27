using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    // 用户动作接口实例
    IUserAction userAction;
    public string message;
    GUIStyle msgStyle, titleStyle;
    bool isGameEnd;

    public void SetMessage(string msg) { message = msg; }
    public bool IsGameEnd
    {
        set { isGameEnd = value; }
    }

    void Start()
    {
        // 侧向转型实例化用户动作对象
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
        // 设置一些style
        msgStyle = new GUIStyle();
        msgStyle.normal.textColor = Color.red;
        msgStyle.fontSize = 40;

        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontSize = 50;

        isGameEnd = false;
    }

    void OnGUI()
    {
        // 设置游戏标题
        GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.1f, 50, 200), "Preists and Devils", titleStyle);
        // 设置游戏的提示信息
        GUI.Label(new Rect(Screen.width * 0.4f, 150, 50, 200), message, msgStyle);
        // 重新开始
        if (isGameEnd && GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.5f, 100, 50), "Restart"))
        {
            Restart();
        }
    }

    void Restart()
    {
        userAction.Restart();
    }
}
