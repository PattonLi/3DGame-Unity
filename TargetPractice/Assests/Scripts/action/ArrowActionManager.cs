using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowActionManager : SSActionManager, ISSActionCallback
{
    public FirstController firstController;

    public void ShootDisk(GameObject disk, float speed, Vector3 direction)
    {
        SSAction ac;
        // run action
        ac = ArrowAction.GetSSAction(direction, speed);
        RunAction(disk, ac, this);
    }

    protected new void Start()
    {
        
    }

    // specific manager do callback
    public void SSActionEvent(SSAction source,
                    SSActionEventType events = SSActionEventType.Competeted,
                    int intParam = 0,
                    string strParam = null,
                    GameObject objectParam = null)
    {
        //»ØÊÕ·Éµú
        //source is the class success the SSAction, in this case is obj of NphysicalDiskAction Class
        //Singleton<RoundController>.Instance.FreeDisk(source.gameobject);
    }
}
