using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineHandler : MonoBehaviour
{
    public Rigidbody target;
    public float power;
    public Vector3 direction;
    public float buildup, breakdown;
    public float currentLevel, currentSetLevel;
    public float condition;
    public destructible health;
    
    // Start is called before the first frame update
    void Start()
    {
        buildup = 8f;
        breakdown = 20f;
        currentLevel = 0;
        currentSetLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.dying)
            condition = 0;
        else
        {

            condition = health.health / health.maxhealth;

            //przybliż currentlevel do currentsetlevel zależnie od buildup i deltatime;
            if (currentLevel != currentSetLevel)
                if (currentLevel > currentSetLevel)
                    currentLevel = Mathf.Max(currentLevel - breakdown * Time.deltaTime, currentSetLevel);
                else
                    currentLevel = Mathf.Min(currentLevel + buildup * Time.deltaTime, currentSetLevel);
        }
        //dodajdopalacz jesli currentlevel
    }
    private void FixedUpdate()
    {
        if (currentLevel > 0)
            target.AddForceAtPosition(transform.TransformVector(direction) * currentLevel * power / 100, transform.position, ForceMode.Impulse);


    }


    public void applyThrust(float level)
    {
        if (!health.dying)
            currentSetLevel = level * condition;
        else
            currentSetLevel = 0;
        /*
        currentLevel += level * buildup;
        currentLevel = Mathf.Min(currentLevel, 1);
        target.AddForceAtPosition(transform.TransformVector(direction) * currentLevel * power/100, transform.position, ForceMode.Impulse);
        */
    }
    public void instaSet()
    {
        currentLevel = currentSetLevel;
    }
    public Vector3 getAngle()
    {
        Vector3 output = new Vector3(0, 0, 0);
        if (direction.x == 1)
            output.z = 180;
        //   if(direction.x==-1)
        //  output.x = 180;
        if (direction.y == 1)
        { 
        output.z = 90;
        output.x = 180; }
        if (direction.y == -1) 
        { 
            output.z = 90;
        }


        if (direction.z == 1)
        {
            output.x = -180;
            output.y = 90;
        }
        if (direction.z == -1)
        {
            output.x = -180;
            output.y =- 90;
        }

        return output;
    }
}
