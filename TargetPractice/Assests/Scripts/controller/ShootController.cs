using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    float force;// ��������
    const float maxForce = 1f;  // �������
    const float chargeRate = 0.1f; // ÿ0.3����������
    Animator animator;//����������
    float mouseDownTime;// ��¼�������ʱ��
    bool isCharging=false;//�Ƿ���������
    public Slider Powerslider;//������
    public SpotController currentSpotController;

    public bool readyToShoot = false;//�Ƿ���Կ�ʼ���
    public int shootNum = 0;// ʣ����ƴ���

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

        //������갴�µ�ʱ��������ÿ0.3����0.1���������0.5)�ӵ�animator��power�����ϣ�������Ӧ�������
        if (Input.GetMouseButtonDown(0)) // 0��ʾ������
        {
            mouseDownTime = Time.time;  // ��¼��갴�µ�ʱ��
            isCharging = true;  // ��ʼ����
            Powerslider.gameObject.SetActive(true);//��ʾ������
        }

        if (isCharging)
        {
            float holdTime = Time.time - mouseDownTime; // ������갴�µ�ʱ��
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce); // �����������������Ϊ0.5
            Powerslider.value = force / maxForce; // ������������ֵ
            animator.SetFloat("Power", force);
        }

        //��굯��
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;  // ֹͣ����
            animator.SetTrigger("Fire");
            Debug.Log("set trigger fire");
            float holdTime = Time.time - mouseDownTime;  // ������갴�µ�ʱ��
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce);  // �����������������Ϊ0.5
            animator.SetFloat("Power", force);  // �����������ӵ�animator��power������
            StartCoroutine(DelayedFireCoroutine(force));//�ӳ�0.5s�����
            Powerslider.value = 0;//����������
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

    //Э�̣�����
    IEnumerator DelayedFireCoroutine(float f)
    {
        Debug.Log("Ready to fire!!");
        yield return new WaitForSeconds(0.2f);//�ȴ�0.2s��
        fire(f);
        
    }

    //���
    public void fire(float f)
    {
        // Your existing fire code
        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Arrow"));
        ArrowFeature arrowFeature = arrow.AddComponent<ArrowFeature>();
        // ʹ��Find����ͨ���Ӷ�������ֻ�ȡarrow�Ӷ���
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
        // ��ȡ�������
        //Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // �����ʼ�ٶ�����
        arrow_db.velocity = transform.forward * 100 * f;


    }
}
