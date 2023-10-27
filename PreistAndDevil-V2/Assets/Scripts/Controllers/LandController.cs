using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController
{
    private Land landModel;
    // 创建land模型
    public void CreateLand(Vector3 position)
    {
        if (landModel == null)
        {
            landModel = new Land(position);
        }
    }
    public Land GetLand()
    {
        return landModel;
    }

    /* 添加或移除人物 */
    public Vector3 AddRole(Role roleModel)
    {
        // 设置parent transform
        roleModel.role.transform.parent = landModel.land.transform;
        roleModel.inBoat = false;
        if (roleModel.isPriest) landModel.priestCount++;
        else landModel.devilCount++;
        // 返回陆地上的目标相对位置
        return DefaultPosition.role_land[roleModel.id];
    }
    public void RemoveRole(Role roleModel)
    {
        if (roleModel.isPriest) landModel.priestCount--;
        else landModel.devilCount--;
    }
}
