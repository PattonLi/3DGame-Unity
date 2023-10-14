using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 船只模型
public class Boat
{
    //船对象
    public GameObject boat;
    //关联船上人员
    public Role[] roles;
    public bool isRight;
    public int priestCount, devilCount;

    public Boat(Vector3 position)
    {
        // 载入船预制件
        boat = GameObject.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject))) as GameObject;
        // 设置属性
        boat.name = "boat";
        boat.transform.position = position;
        boat.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        // 人员对象创建
        roles = new Role[2];

        isRight = false;
        priestCount = devilCount = 0;

        // 添加碰撞盒与点击组件
        boat.AddComponent<BoxCollider>();
        boat.AddComponent<Click>();

    }
}
