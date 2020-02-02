using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float enemySpawnCooldown;

    public float minimumDistance;
    public float maximumDistance;

    private GameObject enemyShip;
    // Start is called before the first frame update
    void Start()
    {
        enemyShip = Resources.Load<GameObject>("Prefabs/EnemyFalcon");
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(enemySpawnCooldown);
        Instantiate(enemyShip, GetSpawnPosition(), Quaternion.identity);
        yield return StartCoroutine(Spawner());
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 position = transform.position;
        position.x += Random.Range(minimumDistance, maximumDistance);
        position.z += Random.Range(-maximumDistance, maximumDistance);

        if (Random.Range(0, 10) >= 5)
        {
            position.x = -position.x;
        }
        
        if (Random.Range(0, 10) >= 5)
        {
            position.z = -position.z;
        }
        return position;
    }
}
