using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 点击模型
public class Click : MonoBehaviour
{
    // 关联用户动作模型
    public IClickAction ClickAction { get; set; }
    void OnMouseDown()
    {
        ClickAction.OnClick();
    }
}
