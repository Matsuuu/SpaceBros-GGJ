using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFalcon : EnemyShip
{
    private GameObject laser;
    private bool canShoot;
    public float cooldown;
    
    public void Start()
    {
        base.Start();
        laser = Resources.Load<GameObject>("Prefabs/EnemyLaser");
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
        Instantiate(laser, transform.position, transform.rotation);
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
