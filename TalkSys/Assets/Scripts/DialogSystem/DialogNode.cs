using System;
using UnityEngine;


// 代表了一个对话节点。
[Serializable]
public class DialogNode
{
    [Header("角色的名字")]
    public string name;
    [Header("角色的头像")]
    public Sprite sprite;
    
    [TextArea, Header("对话的内容")]
    public string content;
}