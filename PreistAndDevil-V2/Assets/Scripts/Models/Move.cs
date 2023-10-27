using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isMoving = false;
    public float speed = 5;

    // 需要经过的轨迹点
    public Vector3 destination;
    public Vector3 mid_destination;

    // Update is called once per frame
    void Update()
    {
        // 已到达终点 do nothing
        if (transform.localPosition == destination)
        {
            isMoving = false;
            return;
        }
        // else moving
        else if (transform.localPosition.x != destination.x && transform.localPosition.y != destination.y)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, mid_destination, speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        }
        isMoving = true;
    }
}
