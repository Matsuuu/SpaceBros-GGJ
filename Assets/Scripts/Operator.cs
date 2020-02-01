using System;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public bool operating = false;
    public Operatable operatableInVicinity = null;
    public PlayerScript playerScript;

    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HandleOperating();
        }
    }

    private void HandleOperating()
    {
        if (operating)
        {
            StopOperating();
        }
        else
        {
            StartOperating();
        }
    }

    public void StartOperating()
    {
        if (operatableInVicinity == null)
        {
            return;
        }

        operating = true;
        operatableInVicinity.StartOperating(this);
        playerScript.canMove = false;
    }

    public void StopOperating()
    {
        operating = false;
        playerScript.canMove = true;
        operatableInVicinity.StopOperating();
    }
}
