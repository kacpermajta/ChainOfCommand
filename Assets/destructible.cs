using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{

    public float health, maxhealth;
    public GameObject flash;
    public MeshRenderer[] healthbar;
    public bool dying, isShip;
    public ParticleSystem[] dmgIndicatorParent;
    public ParticleSystem.EmissionModule[] dmgIndicator;

    public Material active, inactive;
    // Start is called before the first frame update
    void Start()
    {
        dmgIndicator = new ParticleSystem.EmissionModule[dmgIndicatorParent.Length];
        for (int i = 0; i < dmgIndicatorParent.Length; i++)
        {
            dmgIndicator[i] = dmgIndicatorParent[i].emission;
        }
        dying = false;
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDmg(float amount)
    {
        
        //Debug.Log("boli");
        health -= amount;
        if(health<0 && !dying)
        {
            dying = true;
            GameObject flashobject = GameObject.Instantiate(flash, transform.position, transform.rotation);
            Destroy(flashobject, 2);

            //wyrzucić zniszczony statek z listy
            if (isShip)
            {
                GameController.RemoveFromList(gameObject);



                Destroy(gameObject);
            }
        }
        else
        {
            for (int i = 0; i < dmgIndicator.Length; i++) //(ParticleSystem.EmissionModule indicator in dmgIndicator)
            {
                dmgIndicator[i].rateOverTime = 10 * (health / maxhealth);
            }
            if (isShip)
            {
                int activelights = (int)health / healthbar.Length;
                for (int i = 0; i < healthbar.Length; i++)
                {
                    if (i < activelights)
                        healthbar[i].material = active;
                    else
                        healthbar[i].material = inactive;
                }
            }
        }
    }
}
