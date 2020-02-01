using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float maxMovementSpeed;

    public float currentMovementSpeed;

    public float speedChangeIncrement;

    public bool hasMovement;

    public bool canMove;
    public int playerNum;


    // Control keys
    public string horizontal;
    public string vertical;
    public string use;
    public string mount;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!canMove)
        {
            return;
        }
        float x = Input.GetAxis(horizontal);
        float y = Input.GetAxis(vertical);
        if (HasMovement(x, y))
        {
            MoveUser(x, y);
        }
        else
        {
            DecreaseSpeed();
        }
    }

    private bool HasMovement(float x, float y)
    {
        hasMovement = x >= 0.20f || x <= -0.20f || y >= 0.20f || y <= -0.20f;
        return hasMovement;
    }

    private void MoveUser(float x, float y)
    {
        if (currentMovementSpeed <= maxMovementSpeed)
        {
            currentMovementSpeed += speedChangeIncrement;
        }
        //transform.position += transform.right * currentMovementSpeed * Time.deltaTime * x;
        transform.position += new Vector3(x, 0, y) * currentMovementSpeed * Time.deltaTime;
    }

    private void DecreaseSpeed()
    {
        if (currentMovementSpeed >= 0)
        {
            currentMovementSpeed -= speedChangeIncrement;
        }
    }
}
