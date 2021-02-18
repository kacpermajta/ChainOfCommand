using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipDrive : MonoBehaviour
{
    // Start is called before the first frame update

    public AgentInput shipController;
    public Rigidbody characterPhysics;
    public float sideways, vertical, forward, spin, turn, rise;
    public Vector3 shipAbsVelocity, shipVelocity;
    public float handling;
    public float shipHandling;
    int engineAmount;

    public Gradient[]particleColors;
    public ParticleSystem[] exhausts;

    public engineHandler[] engineSet;
    public destructible dampenerMod;

    public float[] forwardSetting;
    public float[] backSetting;
    public float[] leftSetting;
    public float[] rightSetting;
    public float[] upSetting;
    public float[] downSetting;
    public float[] clockwiseSetting;
    public float[] counterclockwiseSetting;


    void Start()
    {
        engineAmount = engineSet.Length;
        setfaction();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipController.dampen && !dampenerMod.dying)
            handling = shipHandling * 0.5f;
        else
            handling = shipHandling * 1;


            sideways = 0;
        vertical = 0;
        forward = 0;
        spin = 0;
        turn = 0;
        rise = 0;


        for (int i = 0; i < engineAmount; i++)
        {

            float combinedLevel = Mathf.Min( shipController.throttle * forwardSetting[i] + shipController.halt * backSetting[i] +
                shipController.left * leftSetting[i] + shipController.right * rightSetting[i] + shipController.clockwise * clockwiseSetting[i] +
                shipController.counterclockwise * counterclockwiseSetting[i] + shipController.up * upSetting[i] + shipController.down * downSetting[i] , 1);
            engineSet[i].applyThrust(handling * combinedLevel);
            if (dampenerMod.dying || !shipController.dampen )
            {
                engineSet[i].instaSet();
            }

        }




        shipAbsVelocity = characterPhysics.velocity;
        shipVelocity = transform.InverseTransformVector(shipAbsVelocity);


            if (Mathf.Abs(shipVelocity.y) > 0.1f && shipController.dampen&& !dampenerMod.dying)
            vertical += 10 * shipVelocity.y;
            if (Mathf.Abs(shipVelocity.x)>0.1f&&shipController.dampen && !dampenerMod.dying)
                sideways -=  10*shipVelocity.x;

        

        characterPhysics.AddRelativeForce(new Vector3(sideways, -vertical, 0));
        //characterPhysics.AddRelativeTorque(new Vector3(-rise*handling, turn * handling, spin * handling));
    }
    public void setfaction()
    {
        foreach (ParticleSystem exhaust in exhausts)
        {
            var thisColorOL = exhaust.colorOverLifetime;
            Gradient grad = particleColors[shipController.faction];
            thisColorOL.color = grad;
        }
    }
}
