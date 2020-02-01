using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceShipBuilder : MonoBehaviour
{
    public float spawnerDelay;
    public int shipColumnCount;

    public int shipRowCount;
    public int doorSpawnPossibility;

    private GameObject shipPart;
    // Start is called before the first frame update
    void Start()
    {
        shipPart = Resources.Load<GameObject>("Prefabs/SpaceShipPart");
        StartCoroutine(BuildShip());
    }

    IEnumerator BuildShip()
    {
        Vector3 spawnPos = transform.position;
        Vector3 startPos = spawnPos;
        Quaternion defaultRotation = Quaternion.Euler(Vector3.zero);
        for (int c = 0; c < shipColumnCount; c++) // Nah not here bruh
        {
            int horizontalDoorCount = 0;
            List<SpaceShipPart> partsInColumn = new List<SpaceShipPart>();
            for (int r = 0; r < shipRowCount; r++)
            {
                GameObject spaceShipPart = Instantiate(shipPart, spawnPos, defaultRotation, transform.parent);

                SpaceShipPart ssp = spaceShipPart.GetComponent<SpaceShipPart>();
                horizontalDoorCount += CalculatePossibleDoors(r, c, ssp);
                partsInColumn.Add(ssp);
                
                spawnPos -= new Vector3(0,0,  shipPart.transform.localScale.z);
                yield return new WaitForSeconds(spawnerDelay);
            }
            if (horizontalDoorCount < 1)
            {
                partsInColumn[Random.Range(0, shipRowCount - 1)].SpawnDoor(c < shipColumnCount - 1 ? 3 : 1);
            }

            HandleAttachments(c, partsInColumn);
            spawnPos = startPos - new Vector3(c * shipPart.transform.localScale.x, 0, 0);
            spawnPos -= new Vector3(shipPart.transform.localScale.x, 0, 0);
        }
    }

    private void HandleAttachments(int column, List<SpaceShipPart> sspList)
    {
        if (column == shipColumnCount - 1)
        {
            SpaceShipPart spaceShipPart = sspList[Random.Range(0, sspList.Count)];

            spaceShipPart.attachmentSpawner.spawnsWithBooster = true;
            spaceShipPart.attachmentSpawner.Spawn();
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    private int CalculatePossibleDoors(int rowCount, int columnCount, SpaceShipPart ssp)
    {
        bool[] doors = new bool[4];
        doors[0] = rowCount != 0;
        doors[1] = columnCount != 0;
        doors[2] = rowCount < shipRowCount -1;
        doors[3] = columnCount < shipColumnCount - 1;

        ssp.possibleDoors = doors;
        ssp.doorSpawnPossibility = doorSpawnPossibility;
        return ssp.Init();
    }
}
