using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceScript : MonoBehaviour
{
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (material.mainTextureOffset.x > -40)
        {
            material.mainTextureOffset -= new Vector2(0.01f, Random.Range(0, 0.02f));
        }
    }
}
