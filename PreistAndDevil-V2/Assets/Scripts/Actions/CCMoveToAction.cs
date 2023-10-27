using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移动的动作具体实现类
public class CCMoveToAction : SSAction
{
    public Vector3 target;   // 移动后的目标位置
    public float speed;

    private CCMoveToAction()
    {

    }
    // Start is called before the first frame update
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, speed * Time.deltaTime);
        // 如果游戏对象不存在或者当前位置已在目标位置上，则不移动
        if (this.transform.localPosition == target || this.gameobject == null)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
            return;
        }
    }

    public static CCMoveToAction GetSSAction(Vector3 target, float speed)
    {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }
}
