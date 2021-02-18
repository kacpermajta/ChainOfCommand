using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{

    public AgentInput gunController;
    public GameObject missile;
    public float cooldown, fireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown < 0)
            cooldown = 0;
        if (gunController.shoot && cooldown == 0f)
        {
            
            GameObject.Instantiate(missile,transform.position,transform.rotation).GetComponent<missileScript>().setFaction(gunController.faction);
            cooldown = fireRate;
        }
    }
}
