using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{

    public bool rotating;
    public AgentInput shipController;
    public Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shipController.looking)
        {
            if(rotating)
            {
                Vector3 difference = Input.mousePosition - startingPos;
                transform.localRotation = Quaternion.Euler(-difference.y*0.6f, difference.x * 0.3f, 0);
            }
            else
            {
                rotating = true;
                startingPos = Input.mousePosition;
            }
        }
        else
        {
            if (rotating)
            {
                transform.localRotation = Quaternion.identity;
                rotating = false;
            }

        }

        
    }
}
