using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operatable : MonoBehaviour
{
    public Operator personOperating = null;

    public bool isBeingOperatedOn = false;

    public List<Operator> operatorsInVicinity;

    public Vector3 operatorIconSpawnLocation;
    public Quaternion operatorIconSpawnRotation;
    public GameObject operateIcon;

    private GameObject operateIconInstance;
    // Start is called before the first frame update
    public void Start()
    {
        operateIcon = Resources.Load<GameObject>("Prefabs/OperateIcon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Operator @operator = other.GetComponent<Operator>();
            operatorsInVicinity.Add(@operator);
            @operator.operatableInVicinity = this;
            CreateOperateIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Operator @operator = other.GetComponent<Operator>();
            @operator.operatableInVicinity = null;
            operatorsInVicinity.Remove(@operator);
            Destroy(operateIconInstance);
        }
    }

    public void StartOperating(Operator @operator)
    {
        personOperating = @operator;
        isBeingOperatedOn = true;
        Destroy(operateIconInstance);
    }

    public void StopOperating()
    {
        personOperating = null;
        isBeingOperatedOn = false;
        CreateOperateIcon();
    }

    private void CreateOperateIcon()
    {
        operateIconInstance = Instantiate(operateIcon, transform.position + operatorIconSpawnLocation,
            operatorIconSpawnRotation, transform);
    }
}
