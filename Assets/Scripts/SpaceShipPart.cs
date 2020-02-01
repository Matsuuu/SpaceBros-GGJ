using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpaceShipPart : MonoBehaviour
{

    private new Renderer renderer;
    public bool isTouchingDoor;

    public bool[] possibleDoors;

    public int doorSpawnPossibility;

    public float doorSpawnOffset;

    public GameObject shipDoor;
    public int horizontalDoorCount = 0;
    public SpaceShipAttachmentSpawner attachmentSpawner;

    private int spawnedDoorCount;
    // Start is called before the first frame update
    void Start()
    {
        attachmentSpawner = GetComponent<SpaceShipAttachmentSpawner>();
        renderer = GetComponent<Renderer>();
        CheckExits();
    }

    public int Init()
    {
        shipDoor = (GameObject) Resources.Load("Prefabs/ShipDoor");
        doorSpawnOffset = 5;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            StartCoroutine(DamageSplash());
            Destroy(other.transform.parent.gameObject);
        }
    }

    IEnumerator DamageSplash()
    {
        Color materialColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        renderer.material.color = materialColor;
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
        Vector3 heightOffset = new Vector3(0, 2, 0);
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
        GameObject newDoor = Instantiate(shipDoor, transform.position + offsetVector * doorSpawnOffset - heightOffset, rotation, transform);
        if (position == 1 || position == 3)
        {
            newDoor.GetComponent<DoorScript>().sideDoor = true;
        }

        spawnedDoorCount++;
    }
}
