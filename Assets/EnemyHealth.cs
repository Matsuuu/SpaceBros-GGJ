using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;
    public GameObject explosionParticles;
    public AudioSource crashSoundMachine;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        explosionParticles = Resources.Load<GameObject>("Prefabs/ExplosionParticles");
        crashSoundMachine = GameObject.Find("CrashSoundMachine").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            HandleDamage(other.transform.parent.GetComponent<Laser>().damage);
        }

        if (other.CompareTag("SpaceShip"))
        {
            HandleDeath();
        }
    }

    private void HandleDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Instantiate(explosionParticles, transform.position, transform.rotation);
        crashSoundMachine.Play();
        Destroy(gameObject);
    }
}
