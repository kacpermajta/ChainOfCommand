using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilizer : MonoBehaviour
{

    public Rigidbody characterPhysics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        characterPhysics.AddRelativeForce(new Vector3(0, 0, 0));
        characterPhysics.AddRelativeTorque(new Vector3(0, 0, 0));
    }
}
