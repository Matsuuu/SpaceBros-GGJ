using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
       /* Vector3 rbVelocity = rb.velocity;
        if (rbVelocity.x > maxSpeed || rbVelocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }

        if (rbVelocity.y > maxSpeed || rbVelocity.y < -maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);
        }

        if (rbVelocity.z > maxSpeed || rbVelocity.z < -maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
        }*/
    }
}
