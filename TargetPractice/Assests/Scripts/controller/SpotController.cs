using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    // 该场景剩余设计数
    public int shootNum;
    //该射击点位对应的靶
    public TargetController[] targetControllers;

    // Start is called before the first frame update
    void Start()
    {
        shootNum = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
