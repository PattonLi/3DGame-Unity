using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;//鼠标移动速度
    private float xRotation = 0.0f;
    private float gravity = 3f;
    public float speed = 0.05f;//移动速度
    public float jumpSpeed = 0.15f;
    private float jumpMovement = 0.0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        /*rorate*/
        // 获取鼠标移动量
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // player rotate by axis Y
        transform.Rotate(Vector3.up * mouseX);
        // camera rotate by axis X
        Camera mainCamera = Camera.main;
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        CharacterController playerController = GetComponent<CharacterController>();

        /*move*/
        // 获取键盘水平移动量
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(H, 0.0f, V);
        //Debug.Log(movement);

        // 从世界坐标系转换到局部坐标系
        movement = Camera.main.transform.rotation * movement * speed;
        Rigidbody rb = GetComponent<Rigidbody>();
        movement = new Vector3(movement.x, 0, movement.z);


        /*jump*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpMovement = jumpSpeed;
        }

        jumpMovement = jumpMovement - gravity * Time.deltaTime;
        movement.y = jumpMovement;
        playerController.Move(movement);



    }


}