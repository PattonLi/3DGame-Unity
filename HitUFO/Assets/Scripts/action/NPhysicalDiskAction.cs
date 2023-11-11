using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPhysicalDiskAction : SSAction
{
    float gravity;
    float time;
    float speed;
    Vector3 direction;

    // builder, get specific actions success SSAction
    public static NPhysicalDiskAction GetSSAction(Vector3 direction, float speed)
    {
        NPhysicalDiskAction action = ScriptableObject.CreateInstance<NPhysicalDiskAction>();
        action.gravity = 9.8f;
        action.time = 0;
        action.speed = speed;
        action.direction = direction;
        return action;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        gameobject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        // disk move 运动合成,transform is the one of scriptable object,
        // which will be set equal to gameobject which excecutes this action. 
        transform.Translate(Vector3.down * gravity * time * Time.deltaTime);
        transform.Translate(direction * speed * Time.deltaTime);
        time += Time.deltaTime;
        //Debug.Log("postion of the disk"+gameobject.name+" is "+gameobject.transform.position);
        // 飞碟到达屏幕底部回调
        Camera mainCamera = Camera.main; // 获取主摄像机
        float cameraHeight = mainCamera.orthographicSize * 2f; // 获取相机的视图高度

        // 计算相机视图的上下边界坐标
        float upperBoundary = mainCamera.transform.position.y + cameraHeight / 2f;
        float lowerBoundary = mainCamera.transform.position.y - cameraHeight / 2f;

        //Debug.Log("Upper Boundary: " + upperBoundary);
        //Debug.Log("Lower Boundary: " + lowerBoundary);
        if (this.transform.position.y < lowerBoundary-100 || this.transform.position.y > upperBoundary+100)
        {
            // action destory
            this.destroy = true;
            this.enable = false;
            // action callback
            this.callback.SSActionEvent(this);//source as this
        }

    }
}
