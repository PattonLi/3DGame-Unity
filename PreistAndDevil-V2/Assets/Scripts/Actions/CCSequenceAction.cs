using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSequenceAction : SSAction, ISSActionCallback
{
    // 动作序列
    public List<SSAction> sequence;
    public int repeat = -1;
    public int start = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        // 初始化列表中的动作
        foreach (SSAction action in sequence)
        {
            action.gameobject = this.gameobject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (sequence.Count <= 0)
        {
            return;
        }
        if (sequence.Count > 0 && start < sequence.Count)
        {
            sequence[start].Update();
        }
        else
        {
            return;
        }
    }


    // 构造动作序列对象
    public static CCSequenceAction GetSSAction(int repeat, int start, List<SSAction> sequence)
    {
        CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
        action.repeat = repeat;
        action.start = start;
        action.sequence = sequence;
        return action;
    }

    // 执行完一个动作后回调
    public void SSActionEvent(SSAction source,
                    SSActionEventType events = SSActionEventType.Competeted,
                    int intParam = 0, string strParam = null, Object objectParam = null)
    {
        source.destroy = false;
        this.start++;

        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (this.repeat > 0)
            {
                this.repeat--;
            }
            else
            {
                this.destroy = true;
                this.callback.SSActionEvent(this);
            }
        }
    }

    void OnDestroy()
    {

    }
}
