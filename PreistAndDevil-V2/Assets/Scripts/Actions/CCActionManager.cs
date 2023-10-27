using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    
    public CCMoveToAction boatMovement;
    public CCSequenceAction roleMovement;
    public FirstController controller;
    private bool isMoving = false;

    protected new void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentSceneController;
        controller.actionManager = this;
    }

    public bool CheckMoving()
    {
        return isMoving;
    }

    public void MoveBoat(GameObject boat, Vector3 target, float speed)
    {
        if (isMoving)
            return;
        isMoving = true;
        boatMovement = CCMoveToAction.GetSSAction(target, speed);
        this.RunAction(boat, boatMovement, this);
    }

    public void MoveRole(GameObject role, Vector3 middle_pos, Vector3 target, float speed)
    {
        if (isMoving)
            return;
        isMoving = true;
        SSAction ac1 = CCMoveToAction.GetSSAction(middle_pos, speed);
        SSAction ac2 = CCMoveToAction.GetSSAction(target, speed);
        roleMovement = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> {CCMoveToAction.GetSSAction(middle_pos, speed), CCMoveToAction.GetSSAction(target, speed)});
        this.RunAction(role, roleMovement, this);
    }

    public void SSActionEvent(SSAction source,
                    SSActionEventType events = SSActionEventType.Competeted,
                    int intParam = 0,
                    string strParam = null,
                    Object objectParam = null)
    {
        isMoving = false;
    }
}
