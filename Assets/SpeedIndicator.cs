using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    public Material off, slow, heavy;
    public MeshRenderer forward;
    public MeshRenderer backward;

    public Rigidbody reference;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float indicatedVelocity = reference.transform.InverseTransformDirection(reference.velocity).z;
        if (indicatedVelocity > 20)
        {
            backward.material = off;
            forward.material = heavy;
        }
        else if (indicatedVelocity > 2)
        {
            backward.material = off;
            forward.material = slow;
        }
        else if (indicatedVelocity <-15)
        {
            forward.material = off;
            backward.material = heavy;
        }
        else if (indicatedVelocity < -2)
        {
            forward.material = off;
            backward.material = slow;
        }
        else
        {
            forward.material = off;
            backward.material = off;
        }


    }
}
