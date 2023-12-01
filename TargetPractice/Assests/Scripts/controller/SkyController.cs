using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public Material[] mats;//天盒素材
    private static int index = 0;
    public int changeTime;//更换天空盒子的秒数

    public void ChangeBox()
    {
        Debug.Log("change skybox" + index);
        RenderSettings.skybox = mats[index];
        index++;
        index %= mats.Length;
    }

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}