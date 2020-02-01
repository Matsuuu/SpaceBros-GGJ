using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Operatable
{
    public float turretTurnSpeed;

    public Transform turret;

    public float cooldown;

    public bool canShoot;

    public Quaternion startRotation;

    public float maxY;
    public float minY;

    public List<Transform> laserExits;

    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        base.Start();
        canShoot = true;
        maxY = startRotation.eulerAngles.y + 30;
        minY = startRotation.eulerAngles.y - 30;
        laser = Resources.Load<GameObject>("Prefabs/Laser");
    }

    void Update()
    {
        if (isBeingOperatedOn)
        {
            float h = Input.GetAxis(personOperating.horizontal);
            float rotationY = turret.rotation.eulerAngles.y;
            if (rotationY > startRotation.eulerAngles.y + 50)
            {
                rotationY -= 360;
            }
            if ((h >= 0.2f && rotationY <= maxY) || (h <= -0.2f && rotationY >= minY))
            {
                //turret.Rotate(Vector3.up * (h * Time.deltaTime * turretTurnSpeed));
                turret.Rotate(0, h * Time.deltaTime * turretTurnSpeed, 0 ,Space.Self);
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
        laserExits.ForEach(exit =>
        {
            Instantiate(laser, exit.transform.position, exit.rotation);
        });
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
