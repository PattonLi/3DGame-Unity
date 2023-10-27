using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 河岸模型
public class Land
{
    public GameObject land;
    public int priestCount, devilCount;
    public Land(Vector3 position)
    {
        // 载入预制件
        land = GameObject.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject))) as GameObject;
        land.transform.position = position;
        priestCount = devilCount = 0;
    }
}
