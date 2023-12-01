using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAction : SSAction
{
    private float speed;
    private Vector3 direction;

    // builder
    public static ArrowAction GetSSAction(Vector3 direction, float speed)
    {
        ArrowAction ac = ScriptableObject.CreateInstance<ArrowAction>();
        ac.speed = speed;
        ac.direction = direction;
        return ac;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        // add clash pysical effect
        gameobject.GetComponent<Rigidbody>().isKinematic = false;
        // add init speed
        gameobject.GetComponent<Rigidbody>().velocity = speed * direction;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Debug.Log("postion of the disk"+gameobject.name+" is "+gameobject.transform.position);
        // �ɵ�������Ļ�ײ��ص�
        Camera mainCamera = Camera.main; // ��ȡ�������
        float cameraHeight = mainCamera.orthographicSize * 2f; // ��ȡ�������ͼ�߶�

        // ���������ͼ�����±߽�����
        float upperBoundary = mainCamera.transform.position.y + cameraHeight / 2f;
        float lowerBoundary = mainCamera.transform.position.y - cameraHeight / 2f;

        //Debug.Log("Upper Boundary: " + upperBoundary);
        //Debug.Log("Lower Boundary: " + lowerBoundary);
        if (this.transform.position.y < lowerBoundary - 100 || this.transform.position.y > upperBoundary + 100)
        {
            // action destory
            this.destroy = true;
            this.enable = false;
            // action callback
            this.callback.SSActionEvent(this);//source as this
        }

    }
}
