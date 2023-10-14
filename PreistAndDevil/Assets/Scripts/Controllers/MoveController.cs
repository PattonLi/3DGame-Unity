using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    GameObject moveObject;
    public bool GetIsMoving()
    {
        return (moveObject != null && moveObject.GetComponent<Move>().isMoving == true);
    }

    // 移动物体
    public void MoveObj(GameObject moveObject, Vector3 destination)
    {
        Move test;
        this.moveObject = moveObject;
        if (!moveObject.TryGetComponent<Move>(out test))
        {
            moveObject.AddComponent<Move>();
        }

        this.moveObject.GetComponent<Move>().destination = destination;

        // 根据欧氏距离移动
        if (this.moveObject.transform.localPosition.y > destination.y)
        {
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(destination.x, this.moveObject.transform.localPosition.y, destination.z);
        }
        else
        {
            this.moveObject.GetComponent<Move>().mid_destination = new Vector3(this.moveObject.transform.localPosition.x, destination.y, destination.z);
        }
    }
}
