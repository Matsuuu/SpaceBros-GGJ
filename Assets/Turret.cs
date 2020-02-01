using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Operatable
{
    public float turretTurnSpeed;

    public Transform turret;

    public float cooldown;

    public bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        
        base.Start();
    }

    void Update()
    {
        if (isBeingOperatedOn)
        {
            
            float y = Input.GetAxis(personOperating.vertical);
            float rotationY = turret.rotation.y;
            if ((y >= 0.2f && rotationY <= 0.3f) || (y <= 0.2f && rotationY >= -0.3f))
            {
                turret.Rotate(Vector3.right * (y * Time.deltaTime * turretTurnSpeed));
            }

            if (personOperating.IsOperatingButtonBeingPressed() && canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        Debug.Log("Pew");
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
