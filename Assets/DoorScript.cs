using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public List<Collider> touchingWalls;

    public Collider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            touchingWalls.Add(other.GetComponent<Collider>());
        }

        if (other.CompareTag("Player"))
        {
            touchingWalls.ForEach(wall => Physics.IgnoreCollision(wall, playerCollider));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingWalls.ForEach(wall => Physics.IgnoreCollision(wall, playerCollider, false));
        }
    }
}
