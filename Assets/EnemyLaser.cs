using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed;

    public float lifeTime;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToDie());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpaceShip"))
        {
            other.GetComponent<ShipHealthSystem>().HandleDamage(damage);
            Destroy(gameObject);
        }
    }
}