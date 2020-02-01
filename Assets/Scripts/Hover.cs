using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverAmount;
    public float hoverSpeed;

    private float startTime;

    public Vector3 hoverDestination;

    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPos = transform.localPosition;
        hoverDestination = transform.localPosition + Vector3.forward * hoverAmount;
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * hoverSpeed;
        float fractionOfJourney = distCovered / hoverAmount;
        transform.localPosition = Vector3.Lerp(startPos, hoverDestination, fractionOfJourney);
        
        if (transform.localPosition == hoverDestination)
        {
            hoverDestination = startPos;
            startPos = transform.localPosition;
            startTime = Time.time;
        }
    }
}
