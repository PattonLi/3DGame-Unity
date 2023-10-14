using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : IClickAction
{
    private Boat boatModel;
    private IUserAction userAction;

    public void OnClick()
    {
        // 前置条件：船没有满员
        // 如果能上船，角色应到的位置就是船所在的位置
        // 如果不能上船，角色应到的位置就是原来的位置

        // 船上有人才能移动
        if (boatModel.roles[0] != null || boatModel.roles[1] != null)
        {
            userAction.MoveBoat();
        }
    }

    public BoatController()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
    }
    public void CreateBoat(Vector3 position)
    {
        boatModel = new Boat(position);
        boatModel.boat.GetComponent<Click>().ClickAction = this;
    }
    public Boat GetBoatModel()
    {
        return boatModel;
    }

    // 人物上船
    public Vector3 AddRole(Role roleModel)
    {
        int index = -1;
        if (boatModel.roles[0] == null) index = 0;
        else if (boatModel.roles[1] == null) index = 1;

        // 无空位不上船
        if (index == -1) return roleModel.role.transform.localPosition;

        // 有空位上船
        boatModel.roles[index] = roleModel;
        roleModel.inBoat = true;
        roleModel.role.transform.parent = boatModel.boat.transform;
        if (roleModel.isPriest) boatModel.priestCount++;
        else boatModel.devilCount++;
        return DefaultPosition.role_boat[index];
    }

    // 将角色从船上移到岸上
    public void RemoveRole(Role roleModel)
    {
        for (int i = 0; i < 2; ++i)
        {
            if (boatModel.roles[i] == roleModel)
            {
                boatModel.roles[i] = null;
                if (roleModel.isPriest) boatModel.priestCount--;
                else boatModel.devilCount--;
                break;
            }
        }
    }


}
