using UnityEngine;

public class ArrowFeature : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 startDir;
    public Transform target;//collider transform
    public float speed;
    public float destoryTime;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destoryTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        destoryTime -= Time.deltaTime;
        if (destoryTime < 0)
        {
            Destroy(this.transform.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
                target = collision.gameObject.transform;
                transform.SetParent(target);
            }
            destoryTime = 5f;

            //set particle effect
            Debug.Log("!!!effect");
            GameObject effect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/BommEffect"));
            effect.transform.position = transform.position;
            effect.transform.SetParent(transform);
        }

    }
}
