using System;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public bool operating = false;
    public Operatable operatableInVicinity = null;
    public PlayerScript playerScript;
    public Rigidbody rb;

    public string use;
    public string mount;
    public string vertical;
    public string horizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (IsMountButtonBeingPressed())
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
        if (operatableInVicinity == null || operatableInVicinity.personOperating != null)
        {
            return;
        }

        rb.isKinematic = true;
        operating = true;
        operatableInVicinity.StartOperating(this);
        playerScript.canMove = false;
    }

    public void StopOperating()
    {
        rb.isKinematic = false;
        operating = false;
        playerScript.canMove = true;
        operatableInVicinity.StopOperating();
    }

    public bool IsOperatingButtonBeingPressed()
    {
        return Input.GetButton(use) || Input.GetAxis(use) >= 0.2f;
    }

    public bool IsMountButtonBeingPressed()
    {
        return Input.GetButtonDown(mount);
    }
}
