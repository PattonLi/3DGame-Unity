using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    // 控制器
    public LandController leftLandController, rightLandController;
    public BoatController boatController;
    public RoleController[] roleControllers;

    // 比较简单不单独创建控制器
    River river;
    // 场景是否正在运行
    public bool isRunning;



    public float time;
    public CCActionManager actionManager;
    public float speed = 5;

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

        time = 60;
    }

    public void MoveBoat()
    {
        if (isRunning == false || actionManager.CheckMoving() == true) return;
        Vector3 target;
        if (boatController.GetBoatModel().isRight)
        {
            target = DefaultPosition.left_boat;
        }
        else
        {
            target = DefaultPosition.right_boat;
        }
        actionManager.MoveBoat(boatController.GetBoatModel().boat, target, speed);
        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;
    }

    public void MoveRole(Role roleModel)
    {
        if (isRunning == false || actionManager.CheckMoving() == true) return;
        Vector3 middle_pos;
        Vector3 target;
        if (roleModel.inBoat)
        {
            if (boatController.GetBoatModel().isRight)
            {
                target = rightLandController.AddRole(roleModel);
            }
            else
            {
                target = leftLandController.AddRole(roleModel);
            }


            middle_pos = new Vector3(roleModel.role.transform.localPosition.x, target.y, target.z);
            actionManager.MoveRole(roleModel.role, middle_pos, target, speed);
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

                target = boatController.AddRole(roleModel);

                middle_pos = new Vector3(target.x, roleModel.role.transform.localPosition.y, target.z);
                actionManager.MoveRole(roleModel.role, middle_pos, target, speed);
            }
        }
    }

    public void Restart()
    {
        SSDirector.ReloadCurrentScene();
    }


    public void JudgeResultCallBack(string result)
    {
        this.gameObject.GetComponent<UserGUI>().SetMessage(result);
        this.gameObject.GetComponent<UserGUI>().IsGameEnd = true;
        this.isRunning = false;

    }

    void Awake()
    {
        SSDirector.GetInstance().CurrentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<JudgeController>();
    }

    void Update()
    {

    }
}