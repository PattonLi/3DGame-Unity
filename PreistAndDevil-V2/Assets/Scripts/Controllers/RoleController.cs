using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实现了点击接口
public class RoleController : IClickAction
{
    // 人物模型
    private Role roleModel;
    // 关联用户操作接口
    IUserAction userAction;
    // 从场记获取SceneController并转型为userAction
    public RoleController()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
    }
    // 创建人物
    public void CreateRole(Vector3 position, bool isPriest, int id)
    {
        roleModel = new Role(position, isPriest, id);
        // 挂载点击动作对象为this,this实现了点击方法
        roleModel.role.GetComponent<Click>().ClickAction = this;
    }
    // 获取role model
    public Role GetRoleModel()
    {
        return roleModel;
    }
    // implement click action
    public void OnClick()
    {
        userAction.MoveRole(roleModel);
    }
}
