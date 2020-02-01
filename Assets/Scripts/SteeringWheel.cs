using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : Operatable
{
    public Transform spaceShip;
    public float turningSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
        base.Start();
        spaceShip = GameObject.FindGameObjectWithTag("SpaceShip").transform;
    }

    void Update()
    {
        if (isBeingOperatedOn)
        {
            float h = Input.GetAxis(personOperating.horizontal);
            if (h >= 0.2f || h <= 0.2f)
            {
                spaceShip.Rotate(Vector3.up * (h * Time.deltaTime * turningSpeed));
            }
        }
    }
}
