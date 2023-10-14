using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 场记实例
public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    // 控制器
    private LandController leftLandController, rightLandController;
    private BoatController boatController;
    private RoleController[] roleControllers;
    private MoveController moveController;

    // 比较简单不单独创建控制器
    River river;
    // 场景是否正在运行
    bool isRunning;

    // 实现加载资源方法
    public void LoadResources()
    {
        // 场景是否正在运行
        isRunning = true;

        // 实例化role Controllers
        roleControllers = new RoleController[6];
        for (int i = 0; i < 6; ++i)
        {
            roleControllers[i] = new RoleController();
            roleControllers[i].CreateRole(DefaultPosition.role_land[i], i < 3 ? true : false, i);
        }

        // 实例化land controller
        leftLandController = new LandController();
        leftLandController.CreateLand(DefaultPosition.left_land);
        leftLandController.GetLand().land.name = "left_land";
        rightLandController = new LandController();
        rightLandController.CreateLand(DefaultPosition.right_land);
        rightLandController.GetLand().land.name = "right_land";

        // 添加人物并放置在左岸  
        foreach (RoleController roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition =
            leftLandController.AddRole(roleController.GetRoleModel());
        }

        // 添加船只控制器
        boatController = new BoatController();
        boatController.CreateBoat(DefaultPosition.left_boat);

        // 创建河流对象
        river = new River(DefaultPosition.river);

        // 创建移动控制器
        moveController = new MoveController();

    }

    // 实现移动船只方法
    public void MoveBoat()
    {
        // 船只在移动无法操作
        if (isRunning == false || moveController.GetIsMoving()) return;
        // 移动方向
        if (boatController.GetBoatModel().isRight)
        {
            moveController.MoveObj(boatController.GetBoatModel().boat, DefaultPosition.left_boat);
        }
        else
        {
            moveController.MoveObj(boatController.GetBoatModel().boat, DefaultPosition.right_boat);
        }
        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;
    }

    // 实现移动人物方法
    public void MoveRole(Role roleModel)
    {
        // 正在移动无操作
        if (isRunning == false || moveController.GetIsMoving()) return;
        // 判断上船还是下船  
        if (roleModel.inBoat)
        {
            // 判断上岸是左边还是右边
            if (boatController.GetBoatModel().isRight)
            {
                moveController.MoveObj(roleModel.role, rightLandController.AddRole(roleModel));
            }
            else
            {
                moveController.MoveObj(roleModel.role, leftLandController.AddRole(roleModel));
            }
            roleModel.onRight = boatController.GetBoatModel().isRight;
            boatController.RemoveRole(roleModel);
        }
        else
        {
            if (boatController.GetBoatModel().isRight == roleModel.onRight)
            {
                if (roleModel.onRight)
                {
                    rightLandController.RemoveRole(roleModel);
                }
                else
                {
                    leftLandController.RemoveRole(roleModel);
                }
                moveController.MoveObj(roleModel.role, boatController.AddRole(roleModel));
            }
        }
    }

    // 实现判断游戏是否结束方法
    public bool Check()
    {
        // 未在运行
        if (isRunning == false) return true;
        this.gameObject.GetComponent<UserGUI>().SetMessage("");

        // 胜利条件
        if (rightLandController.GetLand().priestCount == 3)
        {
            this.gameObject.GetComponent<UserGUI>().SetMessage("你胜利了!");
            isRunning = false;
            return true;
        }

        // 获取两边的人员数量
        int leftPriestCount, rightPriestCount, leftDevilCount, rightDevilCount;
        leftPriestCount = leftLandController.GetLand().priestCount +
        (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().priestCount);
        rightPriestCount = rightLandController.GetLand().priestCount +
        (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().priestCount : 0);
        leftDevilCount = leftLandController.GetLand().devilCount +
        (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().devilCount);
        rightDevilCount = rightLandController.GetLand().devilCount +
        (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().devilCount : 0);
        // 失败条件
        if (leftPriestCount != 0 && leftPriestCount < leftDevilCount ||
         rightPriestCount != 0 && rightPriestCount < rightDevilCount)
        {
            this.gameObject.GetComponent<UserGUI>().SetMessage("Game Over!");
            isRunning = false;
            return true;
        }

        return false;
    }

    public void Restart()
    {
        SSDirector.ReloadCurrentScene();
    }

    // 在游戏对象被实例化后立即调用
    void Awake()
    {
        SSDirector.GetInstance().CurrentSceneController = this;
        LoadResources();
        // 不添加GUI组件无法显示
        this.gameObject.AddComponent<UserGUI>();
    }

}
