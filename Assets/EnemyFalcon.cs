using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFalcon : EnemyShip
{
    private GameObject laser;
    private bool canShoot;
    public float cooldown;
    public AudioSource laserSoundMachine;
    
    public void Start()
    {
        base.Start();
        laser = Resources.Load<GameObject>("Prefabs/EnemyLaser");
        laserSoundMachine = GameObject.Find("LaserSoundMachine").GetComponent<AudioSource>();
        canShoot = true;
    }

    public override void HandleAttack()
    {
        if (canShoot && IsAtEngageDistance())
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Vector3 shootRotation = transform.rotation.eulerAngles;
        laserSoundMachine.Play();
        Quaternion newRotation = Quaternion.Euler(new Vector3(shootRotation.x, shootRotation.y + Random.Range(-15, 15), shootRotation.z));
        Instantiate(laser, transform.position, newRotation);
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
