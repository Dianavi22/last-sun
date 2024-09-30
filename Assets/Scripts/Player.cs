using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isMove = true;
    public float slowDown = 0.2f;
    Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMove)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, slowDown * Time.deltaTime);

            if (rb.velocity.magnitude < 0.01f)
            {
                rb.velocity = Vector3.zero;
                isMove = false;
            }
        }
    }
}
