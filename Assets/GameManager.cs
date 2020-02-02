using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int detachedParts = 0;

    public int totalParts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementDetachedParts()
    {
        detachedParts++;
        if (totalParts - detachedParts == 1)
        {
            SceneManager.LoadScene("Game_Over");
        }
    }
}
