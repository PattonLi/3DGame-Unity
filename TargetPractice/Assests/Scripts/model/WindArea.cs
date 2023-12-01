using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float m_Radius;
    public float m_Force;

    void Start()
    {
        m_Radius = 4f;
        m_Force = 40f;
    }

    void FixedUpdate()
    {
        Collider[] colliders;
        Rigidbody rigidbody;
        colliders = Physics.OverlapSphere(transform.position, m_Radius);

        foreach (Collider collider in colliders)
        {
            rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            if (rigidbody == null)
            {
                continue;
            }
            rigidbody.AddExplosionForce(m_Force * -1, transform.position, m_Radius);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
        
    }
}
