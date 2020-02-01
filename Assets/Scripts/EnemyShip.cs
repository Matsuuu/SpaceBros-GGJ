using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    public float engageDistance;

    public float flySpeed;

    public Transform ship;
    // Start is called before the first frame update
    public void Start()
    {
        ship = GameObject.FindGameObjectWithTag("SpaceShip").transform;
        engageDistance = Random.Range(engageDistance - 5, engageDistance + 5);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoving();
        HandleRotation();
        HandleAttack();
    }

    public virtual void HandleAttack()
    {
        
    }

    protected bool IsAtEngageDistance()
    {
        return Vector3.Distance(transform.position, ship.position)  <= engageDistance;
    }

    private void HandleMoving()
    {
        if (!IsAtEngageDistance())
        {
            transform.position = Vector3.MoveTowards(transform.position, ship.position, flySpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        transform.LookAt(ship.transform);
    }
}
