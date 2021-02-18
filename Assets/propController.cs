using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propController : MonoBehaviour
{
    // Start is called before the first frame update

    public AgentInput shipController;
    public MeshRenderer activeLed;
    public AgentInput.mode propMode;

    public Material active, inactive;
    public bool working, gui;

    public float scale;
    public float maxScale;
    public Transform Scaling;

    void Start()
    {
        if(gui)
            Scaling.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            if (gui && propMode != shipController.GUIMode)
            {
                setLed(false);
                working = false;

            }

            if (!gui && propMode != shipController.playerMode)
            {
                setLed(false);
                working = false;

            }
        }
        
    }
    void FixedUpdate()
    {
        if (gui)
        {
            if (working)
            {
                scale = Mathf.Min(maxScale, scale + Time.deltaTime);
            }
            else
            {
                scale = Mathf.Max(0.1f, scale - Time.deltaTime);
            }
            Scaling.localScale = new Vector3(scale, scale, scale);
        }


    }

    public void activate()
    {
        working = true;
        setLed(true);
        if (gui)
            shipController.GUIMode = propMode;
        else
            shipController.playerMode = propMode;

    }


    public void setLed(bool value)
    {
        if (activeLed ?? false)
        {
            if (value)
                activeLed.material = active;
            else
                activeLed.material = inactive;
        }
    }

}
