using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operatable : MonoBehaviour
{
    public Operator personOperating = null;

    public bool isBeingOperatedOn = false;

    public List<Operator> operatorsInVicinity;
    public List<Operatable> operatablesInVicinity = new List<Operatable>();

    public Vector3 operatorIconSpawnLocation;
    public float operatorIconSpawnScale;
    public Quaternion operatorIconSpawnRotation;
    public GameObject operateIcon;

    private GameObject operateIconInstance;
    public GameObject smoke;
    public GameObject smokeInstance;
    public bool isToolbox;

    public bool isBroken = false;
    // Start is called before the first frame update
    public void Start()
    {
        operateIcon = Resources.Load<GameObject>("Prefabs/OperateIcon");
        smoke = Resources.Load<GameObject>("Prefabs/Smoke");
        smokeInstance = Instantiate(smoke, transform.position, smoke.transform.rotation, transform);
        smokeInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBroken)
        {
            Operator @operator = other.GetComponent<Operator>();
            if (operatorsInVicinity.Contains(@operator))
            {
                return;
            }
            operatorsInVicinity.Add(@operator);
            @operator.operatableInVicinity = this;
            CreateOperateIcon();
        }

        if (other.CompareTag("Operatable"))
        {
            Operatable operatable = other.GetComponent<Operatable>();
            if (operatablesInVicinity.Contains(operatable))
            {
                return;
            }
            operatablesInVicinity.Add(operatable);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Operator @operator = other.GetComponent<Operator>();
            if (@operator == personOperating || @operator.operating)
            {
                return;
            }
            @operator.operatableInVicinity = null;
            operatorsInVicinity.Remove(@operator);
            Destroy(operateIconInstance);
        }
        if (other.CompareTag("Operatable"))
        {
            Operatable operatable = other.GetComponent<Operatable>();
            operatablesInVicinity.Remove(operatable);
            
        }
    }

    public void StartOperating(Operator @operator)
    {
        if (isBeingOperatedOn)
        {
            return;
        }
        personOperating = @operator;
        personOperating.carryingToolbox = isToolbox;
        isBeingOperatedOn = true;
        Destroy(operateIconInstance);
    }

    public void StopOperating()
    {
        personOperating.carryingToolbox = false;
        personOperating = null;
        isBeingOperatedOn = false;
        CreateOperateIcon();
    }

    private void CreateOperateIcon()
    {
        if (!operateIconInstance && !isBeingOperatedOn && operateIcon != null) 
        {
            operateIconInstance = Instantiate(operateIcon, transform.position + operatorIconSpawnLocation,
                operatorIconSpawnRotation, transform);
            operateIconInstance.transform.localScale = new Vector3(operatorIconSpawnScale,operatorIconSpawnScale,operatorIconSpawnScale);
        }
    }

    public void Break()
    {
        isBroken = true;
        smokeInstance.SetActive(true);
        if (personOperating)
        {
            personOperating.StopOperating();
        }
    }

    public void Fix()
    {
        
    }
}
