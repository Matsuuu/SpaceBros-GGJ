using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public List<Collider> touchingWalls;

    public List<Collider> playerColliders;

    public Transform doorOne;
    public Transform doorTwo;

    public bool sideDoor;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerColliders.Add(player.GetComponent<Collider>());
        }
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
            doorOne.transform.position = doorOne.transform.position + (sideDoor ? Vector3.forward : Vector3.left);
            doorTwo.transform.position = doorTwo.transform.position + (sideDoor ? Vector3.back : Vector3.right);
            
            touchingWalls.ForEach(wall => playerColliders.ForEach(player => Physics.IgnoreCollision(wall, player)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            doorOne.transform.position = doorOne.transform.position - (sideDoor ? Vector3.forward : Vector3.left);
            doorTwo.transform.position = doorTwo.transform.position - (sideDoor ? Vector3.back : Vector3.right);
            
            touchingWalls.ForEach(wall => playerColliders.ForEach(player => Physics.IgnoreCollision(wall, player, false)));
        }
    }
}
