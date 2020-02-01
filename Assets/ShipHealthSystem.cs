using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Game over GG");
    }
}
