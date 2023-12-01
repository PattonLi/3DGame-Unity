using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    float force;// 蓄力力量
    const float maxForce = 1f;  // 最大力量
    const float chargeRate = 0.1f; // 每0.3秒蓄力的量
    Animator animator;//弓动画控制
    float mouseDownTime;// 记录鼠标蓄力时间
    bool isCharging=false;//是否正在蓄力
    public Slider Powerslider;//蓄力条
    public SpotController currentSpotController;

    public bool readyToShoot = false;//是否可以开始射击
    public int shootNum = 0;// 剩余设计次数

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Power", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToShoot)
        {
            Powerslider.gameObject.SetActive(false);
            return;
        }

        //按照鼠标按下的时间蓄力，每0.3秒蓄0.1的力（最多0.5)加到animator的power属性上，并用相应的力射箭
        if (Input.GetMouseButtonDown(0)) // 0表示鼠标左键
        {
            mouseDownTime = Time.time;  // 记录鼠标按下的时间
            isCharging = true;  // 开始蓄力
            Powerslider.gameObject.SetActive(true);//显示蓄力条
        }

        if (isCharging)
        {
            float holdTime = Time.time - mouseDownTime; // 计算鼠标按下的时间
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce); // 计算蓄力的量，最大为0.5
            Powerslider.value = force / maxForce; // 更新力量条的值
            animator.SetFloat("Power", force);
        }

        //鼠标弹起
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;  // 停止蓄力
            animator.SetTrigger("Fire");
            Debug.Log("set trigger fire");
            float holdTime = Time.time - mouseDownTime;  // 计算鼠标按下的时间
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce);  // 计算蓄力的量，最大为0.5
            animator.SetFloat("Power", force);  // 将蓄力的量加到animator的power属性上
            StartCoroutine(DelayedFireCoroutine(force));//延迟0.5s后射击
            Powerslider.value = 0;//清零蓄力条
            animator.SetFloat("Power", 0f);

            //update shootNum
            shootNum--;
            currentSpotController.shootNum--;
            Singleton<UserGUI>.Instance.SetShootNum(shootNum);
            if (shootNum == 0)
            {
                readyToShoot = false;
            }
        }
    }

    //协程：开火
    IEnumerator DelayedFireCoroutine(float f)
    {
        Debug.Log("Ready to fire!!");
        yield return new WaitForSeconds(0.2f);//等待0.2s后
        fire(f);
        
    }

    //射击
    public void fire(float f)
    {
        // Your existing fire code
        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Arrow"));
        ArrowFeature arrowFeature = arrow.AddComponent<ArrowFeature>();
        // 使用Find方法通过子对象的名字获取arrow子对象
        Transform originArrowTransform = transform.Find("mark");
        arrow.transform.position = originArrowTransform.position;
        arrow.transform.rotation = transform.rotation;


        Rigidbody arrow_db = arrow.GetComponent<Rigidbody>();

        // set feature
        arrowFeature.startPos = arrowFeature.transform.position;
        arrow.tag = "Arrow";


        
        Debug.Log("arrow_prefab velocity:" + 100 * f * originArrowTransform.forward);


        //Debug.LogFormat("arrow_prefab Transform info - Position: ({0}, {1}, {2}), Rotation: ({3}, {4}, {5})",
        //            arrow.transform.position.x, arrow.transform.position.y, arrow.transform.position.z,
        //            arrow.transform.rotation.eulerAngles.x, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);
        //Debug.LogFormat("arrow_origin Transform info - Position: ({0}, {1}, {2}), Rotation: ({3}, {4}, {5})",
        //            originArrowTransform.position.x, originArrowTransform.position.y, originArrowTransform.position.z,
        //            originArrowTransform.rotation.eulerAngles.x, originArrowTransform.rotation.eulerAngles.y, originArrowTransform.rotation.eulerAngles.z);
        
        //arrow_db.velocity = 100 * f * originArrowTransform.up;



        //Arrow.transform.position = transform.position;
        //Arrow.transform.rotation = transform.rotation;
        // 获取刚体组件
        //Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // 计算初始速度向量
        arrow_db.velocity = transform.forward * 100 * f;


    }
}
