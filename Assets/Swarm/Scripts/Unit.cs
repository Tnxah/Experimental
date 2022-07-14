using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private Transform core;

    private SphereCollider sphereCollider;

    private Rigidbody rb;

    private float coreDistance;
    private float speed = 200f;

    private bool move = true;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    { if (move)
        {
            Vector3 direction = (core.position - transform.position).normalized;
            rb.velocity = direction * speed * Time.fixedDeltaTime;
            print(direction * speed * Time.fixedDeltaTime);
            print(core.position - transform.position);
        }
        else
        {
            rb.velocity = Vector3.zero;

            Vector3 delta = Vector3.zero - rb.velocity;
            delta *= Time.fixedDeltaTime;

            rb.velocity += delta;
        }
    }

    public float GetCoreDistance()
    {
        coreDistance = Vector3.Distance(core.position, transform.position);
        return coreDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            move = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            move = true;
        }
    }
}
