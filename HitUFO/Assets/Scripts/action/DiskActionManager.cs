using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiskActionManager : SSActionManager, ISSActionCallback
{
    public FirstController firstController;

    public void ShootDisk(GameObject disk, float speed, Vector3 direction,PhysiclaType type)
    {
        SSAction ac;
        // run action
        if (type==  PhysiclaType.noPhysical)
        {
            ac = NPhysicalDiskAction.GetSSAction(direction, speed);
        }
        else if (type == PhysiclaType.pysical)
        {
            ac = PhysicalDiskAction.GetSSAction(direction, speed);
        }
        else
        {
            throw new NotImplementedException();
        }
        
        RunAction(disk, ac, this);
    }

    protected new void Start()
    {
        // get fisrt controller
        firstController = (FirstController)Director.GetInstance().CurrentSceneController;
        // set action manager as this
        firstController.actionManager = this;

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
        Singleton<RoundController>.Instance.FreeDisk(source.gameobject);
    }
}
