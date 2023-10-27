using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人物模型
public class Role
{
    public GameObject role;
    public bool isPriest;
    public bool inBoat;
    public bool onRight;
    public int id;//标识不同的人物

    public Role(Vector3 position, bool isPriest, int id)
    {
        // 设置属性
        this.isPriest = isPriest;
        this.id = id;
        onRight = false;
        inBoat = false;

        // 载入预制件
        role = GameObject.Instantiate(Resources.Load("Prefabs/" + (isPriest ? "Priest" : "Devil"), typeof(GameObject))) as GameObject;
        role.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        role.transform.position = position;
        role.name = "role" + id;

        // 添加点击组件与碰撞盒
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
    }
}
