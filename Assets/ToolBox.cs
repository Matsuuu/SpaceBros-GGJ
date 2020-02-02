using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : Operatable
{

    private Rigidbody rb;
    public GameObject wrenchIcon;
    public AudioSource repairSoundMachine;
    

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        operateIcon = null;
        rb = GetComponent<Rigidbody>();
        repairSoundMachine = GameObject.Find("RepairSoundMachine").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingOperatedOn)
        {
            wrenchIcon.SetActive(false);
            rb.isKinematic = true;
            transform.position = personOperating.transform.position + (Vector3.up * 1.3f);
            if (personOperating.IsOperatingButtonBeingPressed())
            {
                if (operatablesInVicinity.Count > 0)
                {
                    repairSoundMachine.Play();
                    operatablesInVicinity.ForEach(operatable =>
                    {
                        
                    });
                }
            }
        }
        else
        {
            wrenchIcon.SetActive(true);
            rb.isKinematic = false;
        }
    }
}
