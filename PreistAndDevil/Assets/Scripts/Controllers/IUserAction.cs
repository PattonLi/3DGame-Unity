using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 抽象接口，定义用户的行为
public interface IUserAction
{
    void MoveBoat();
    void MoveRole(Role roleModel);
    bool Check();
    void Restart();
}
