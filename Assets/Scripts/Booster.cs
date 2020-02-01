using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : Operatable
{

    public float exhaustTurnSpeed;
    public float turningSpeed;
    public float speed;

    public Transform exhaust;

    public Rigidbody spaceShip;

    public List<ParticleSystem> particleSystems;

    private bool gasStarted;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SetParticleRate(0);
        spaceShip = GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingOperatedOn)
        {
            float y = Input.GetAxis(personOperating.vertical);
            float rotationY = exhaust.rotation.y;
            if ((y >= 0.2f && rotationY <= 0.2f) || (y <= 0.2f && rotationY >= -0.2f))
            {
                exhaust.Rotate(Vector3.right * (y * Time.deltaTime * exhaustTurnSpeed));
            }

            if (personOperating.IsOperatingButtonBeingPressed())
            {
                GasGasGas();
            }
            else
            {
                if (gasStarted)
                {
                    gasStarted = false;
                    SetParticleRate(0);
                }
            }
        }
    }

    private void SetParticleRate(int rate)
    {
        particleSystems.ForEach(ps =>
        {
            ParticleSystem.EmissionModule emissionModule = ps.emission;
            emissionModule.rateOverTime = rate;
        });
    }


    private void GasGasGas()
    {
        if (!gasStarted)
        {
            gasStarted = true;
            SetParticleRate(50);
        }
        float direction = exhaust.rotation.y * -1;
        
        Vector3 force = new Vector3( speed, 0,direction * turningSpeed);
        spaceShip.AddRelativeForce(force);
    }
}
