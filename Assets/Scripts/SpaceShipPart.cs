using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpaceShipPart : MonoBehaviour
{

    private Renderer renderer;
    public bool isTouchingDoor;

    public bool[] possibleDoors;

    public int doorSpawnPossibility;

    public float doorSpawnOffset;

    public GameObject shipDoor;
    public bool mustHaveHorizontalDoor;
    public int horizontalDoorCount;

    private int spawnedDoorCount;
    // Start is called before the first frame update
    void Start()
    {
        CheckExits();
    }

    public int Init()
    {
        shipDoor = (GameObject) Resources.Load("Prefabs/ShipDoor");
        doorSpawnOffset = transform.localScale.x / 2;
        renderer = GetComponent<Renderer>();
        CreateDoors();
        return horizontalDoorCount;
    }

    public void CheckExits()
    {
        if (!isTouchingDoor)
        {
            SpawnRandomDoor();
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (renderer != null)
            {
                Color materialColor = renderer.material.color;
                materialColor.a = 0.2f;
                renderer.material.color = materialColor;
            }
        }

        if (other.CompareTag("Door"))
        {
            isTouchingDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Color materialColor = renderer.material.color;
            materialColor.a = 0.9f;
            renderer.material.color = materialColor;
        }
    }

    private void CreateDoors()
    {
        if (mustHaveHorizontalDoor)
        {
            SpawnDoor(possibleDoors[3] ? 3 : 1);
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            if (!possibleDoors[i])
            {
                continue;
            }

            int randomNum = new Random().Next(0, 100);
            if (randomNum <= doorSpawnPossibility)
            {
                SpawnDoor(i);
            }
        }
    }

    private void SpawnRandomDoor()
    {
        int randomDoorPos = new Random().Next(0, 3);
        while (!possibleDoors[randomDoorPos])
        {
            randomDoorPos = new Random().Next(0, 3);
        }
        SpawnDoor(randomDoorPos);
    }

    public void SpawnDoor(int position)
    {
        Vector3 offsetVector = Vector3.up;
        Quaternion rotation = transform.rotation;
        switch (position)
        {
            case 0:
                offsetVector = Vector3.forward;
                break;
            case 1:
                offsetVector = Vector3.right;
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                if (!possibleDoors[3])
                {
                    horizontalDoorCount++;
                }
                break;
            case 2:
                offsetVector = Vector3.back;
                break;
            case 3:
                offsetVector = Vector3.left;
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                horizontalDoorCount++;
                break;
        }
        Instantiate(shipDoor, transform.position + offsetVector * doorSpawnOffset, rotation, transform);
        spawnedDoorCount++;
    }
}
