using UnityEngine;

// 表示一段对话
[CreateAssetMenu(menuName="创建对话" ,fileName = "对话")]
public class Dialogue : ScriptableObject 
{
    // 对话节点
    public DialogNode[] dialogNodes;
}