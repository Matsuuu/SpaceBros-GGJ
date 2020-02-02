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

    public AudioSource laserSoundMachine;
    // Start is called before the first frame update
    void Start()
    {
        laserSoundMachine = GameObject.Find("LaserSoundMachine").GetComponent<AudioSource>();
        startRotation = transform.localRotation;
        base.Start();
        canShoot = true;
        maxY = 35;
        minY = -35;
        laser = Resources.Load<GameObject>("Prefabs/Laser");
    }

    void Update()
    {
        if (isBeingOperatedOn)
        {
            float h = Input.GetAxis(personOperating.horizontal);
            float rotationY = turret.localRotation.eulerAngles.y;
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
        laserExits.ForEach(exit =>
        {
            laserSoundMachine.Play();
            Instantiate(laser, exit.transform.position, exit.rotation);
        });
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
