using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移动速度")]
    public float speed;
    Animator animator;
    Vector3 movement;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //移动
        movement = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, transform.position.z);
        transform.Translate(movement);

        //动画
        if (movement != Vector3.zero)
        {
            animator.SetBool("run", true);
        }else{
            animator.SetBool("run", false);
        }

        //翻面
        if(movement.x>0){
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(movement.x<0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
