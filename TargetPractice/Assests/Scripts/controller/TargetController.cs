using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class TargetController : MonoBehaviour
{   
    
    public int basepoint;//初始分数
    public bool isMoving;//是否为移动靶
    Animator animator;//动画控制器
    public float aniSpeed = 1f;//动画执行速度
    public int scores;//单个靶点的分数
    // Use this for initialization
    void Start()
    {   
        animator = GetComponent<Animator>();
        this.tag = "Target";
        //设置
        if (isMoving == true)
        {
            animator.SetBool("IsMove", true);
            basepoint = 10;
        }
        else
        {
            animator.SetBool("IsMove", false);
            basepoint = 5;
        }
        //初始分数
        scores= 0;
        animator.speed = aniSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //打到靶子
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("----------------target OnCollisionEnter");
        animator.SetTrigger("Hit");
        // Get the first contact point
        ContactPoint contact = collision.GetContact(0);
        
        // Get the starting_point property from the arrow's script
        // Replace "ArrowScript" with the name of the script that contains the starting_point property
        if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log("--------------is arrow target OnCollisionEnter");
            Vector3 starting_point = collision.gameObject.GetComponent<ArrowFeature>().startPos;

            // Calculate the distance
            float distance = Vector3.Distance(contact.point, starting_point);
            Debug.Log("hitting point:" + contact.point);
            // Print the distance
            Debug.Log("Distance: " + distance);
            Transform center = transform.Find("center");
            Debug.Log("center point:" + center.position);

            //评分乘法因子
            int factor = 0;
            if(Vector3.Distance(contact.point,center.position)<0.09)
            {
                factor = 5;
            }
            else if(Vector3.Distance(contact.point, center.position) < 0.37)
            {
                factor = 3;
            }
            else
            {
                factor = 1;
            }

            //增加该靶分数
            int addScore = basepoint + factor * (int)distance;
            scores += addScore;
            //增加游戏总分数
            Singleton<UserGUI>.Instance.AddScore(addScore);

            //set tip
            string tip = "得分：标准分(" + basepoint + ")+ 奖励因子(" + factor + ") * " +
                " 与射击点距离(" + (int)distance + ") = " + addScore;
            Singleton<UserGUI>.Instance.SetTip(tip, 4);

            
        }
    }
}