using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Director : System.Object
{
    static Director _instance;
    // ��������ʵ��
    public ISceneController CurrentSceneController { get; set; }
    // ��ȡ��ǰ�ĳ���
    public static Director GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }

    public static void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

