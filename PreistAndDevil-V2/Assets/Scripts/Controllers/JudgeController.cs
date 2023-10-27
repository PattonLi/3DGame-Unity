using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 裁判控制器类，判断游戏进行的状态并通知主控制器
// 怎么解决？在FirstCotroller中设计一个回调函数JudgeResultCallBack
public class JudgeController : MonoBehaviour
{
    public FirstController sceneController;
    public Land rightLand;
    public Land leftLand;
    public Boat boat;

    // Start is called before the first frame update
    void Start()
    {
        this.sceneController = (FirstController)SSDirector.GetInstance().CurrentSceneController;
        this.rightLand = sceneController.rightLandController.GetLand();
        this.leftLand = sceneController.leftLandController.GetLand();
        this.boat = sceneController.boatController.GetBoatModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneController.isRunning == false)
        {
            return;
        }
        if (rightLand.priestCount == 3)
        {
            // win, callback
            sceneController.JudgeResultCallBack("You win!!");
            return;
        }
        else
        {
            int leftPriestCount, rightPriestCount, leftDevilCount, rightDevilCount;
            leftPriestCount = leftLand.priestCount + (boat.isRight ? 0 : boat.priestCount);
            rightPriestCount = 3 - leftPriestCount;
            leftDevilCount = leftLand.devilCount + (boat.isRight ? 0 : boat.devilCount);
            rightDevilCount = 3 - leftDevilCount;

            if ((leftPriestCount != 0 && leftPriestCount < leftDevilCount) || (rightPriestCount != 0 && rightPriestCount < rightDevilCount))
            {
                // lose
                sceneController.JudgeResultCallBack("Game over!!");
                return;
            }
        }

    }
}
