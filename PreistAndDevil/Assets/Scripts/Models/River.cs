using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 河流模型
public class River
{
    public GameObject river;
    public River(Vector3 position)
    {
        // 载入预制件
        river = GameObject.Instantiate(Resources.Load("Prefabs/river", typeof(GameObject))) as GameObject;
        river.transform.position = position;
        river.name = "river";
    }
}
