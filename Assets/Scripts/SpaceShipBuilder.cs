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

    private List<SpaceShipPart> allParts = new List<SpaceShipPart>();

    public enum SpaceShipSide
    {
        RIGHT, LEFT, DOWN, UP, NONE
    };
    private List<SpaceShipPart> edgeParts = new List<SpaceShipPart>();
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
                allParts.Add(ssp);
                
                spawnPos -= new Vector3(0,0,  shipPart.transform.localScale.z);
                yield return new WaitForSeconds(spawnerDelay);
                if (c == 0 || c == shipColumnCount - 1 || r == 0 || r == shipRowCount - 1)
                {
                    edgeParts.Add(ssp);
                    ssp.attachmentSpawner.side = GetSide(c, r);
                }
            }
            if (horizontalDoorCount < 1)
            {
                partsInColumn[Random.Range(0, shipRowCount - 1)].SpawnDoor(c < shipColumnCount - 1 ? 3 : 1);
            }

            HandleAttachments(c, partsInColumn);
            spawnPos = startPos - new Vector3(c * shipPart.transform.localScale.x, 0, 0);
            spawnPos -= new Vector3(shipPart.transform.localScale.x, 0, 0);
        }

        SetSteeringWheel();
        SetTurrets();
        allParts.ForEach(part =>
        {
            part.attachmentSpawner.Spawn();
        });
    }

    private SpaceShipSide GetSide(int c, int r)
    {
        if (c == 0)
        {
            return SpaceShipSide.RIGHT;
        }

        if (c == shipColumnCount - 1)
        {
            return SpaceShipSide.LEFT;
        }

        if (r == 0)
        {
            return SpaceShipSide.UP;
        }

        if (r == shipRowCount - 1)
        {
            return SpaceShipSide.DOWN;
        }

        return SpaceShipSide.NONE;
    }

    private void HandleAttachments(int column, List<SpaceShipPart> sspList)
    {
        if (column == shipColumnCount - 1)
        {
            SpaceShipPart spaceShipPart = sspList[Random.Range(0, sspList.Count)];

            spaceShipPart.attachmentSpawner.spawnsWithBooster = true;
        }
    }

    private void SetSteeringWheel()
    {
        SpaceShipAttachmentSpawner spaceShipAttachmentSpawner = allParts[Random.Range(0, allParts.Count)].attachmentSpawner;
        spaceShipAttachmentSpawner.spawnsWithSteeringWheel = true;
        
    }

    private void SetTurrets()
    {
        int turretCount = edgeParts.Count / 2 - 1;
        turretCount = turretCount >= 1 ? turretCount : 1; // Ensure at least one turret
        for (int i = 0; i < turretCount; i++)
        {
            SpaceShipAttachmentSpawner spaceShipAttachmentSpawner = edgeParts[Random.Range(0, edgeParts.Count)].attachmentSpawner;
            while (spaceShipAttachmentSpawner.spawnsWithTurret || spaceShipAttachmentSpawner.spawnsWithBooster)
            {// Make sure no double turret rooms
                spaceShipAttachmentSpawner = edgeParts[Random.Range(0, edgeParts.Count)].attachmentSpawner;
            }
            spaceShipAttachmentSpawner.spawnsWithTurret = true;
        }
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
