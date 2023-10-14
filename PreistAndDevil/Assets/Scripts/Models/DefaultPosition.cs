using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 存储所有object的位置
public class DefaultPosition
{
    // 地形位置
    public static Vector3 left_land = new Vector3(-8, -3.52f, 0);
    public static Vector3 right_land = new Vector3(8, -3.52f, 0);
    public static Vector3 river = new Vector3(0, -4, 0);

    // 船只位置
    public static Vector3 left_boat = new Vector3(-2.3f, -3.5f, -0.4f);
    public static Vector3 right_boat = new Vector3(2.3f, -3.5f, -0.4f);

    // 人物位置
    public static Vector3[] role_land = new Vector3[] { new Vector3(0.4f, 0.9f, 0), new Vector3(0.228f, 0.9f, 0), new Vector3(0.058f, 0.9f, 0), new Vector3(-0.126f, 0.77f, 0), new Vector3(-0.29f, 0.77f, 0), new Vector3(-0.44f, 0.77f, 0) };
    public static Vector3[] role_boat = new Vector3[] { new Vector3(0.3f, 1, 0), new Vector3(-0.3f, 1, 0) };
}
