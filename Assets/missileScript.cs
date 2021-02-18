using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileScript : MonoBehaviour
{
    public GameObject flash;
    public Rigidbody missilePhysics;
    float damage = 10;
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        missilePhysics.AddRelativeForce(new Vector3(0, 0, 2650f));
    }

    // Update is called once per frame
    void Update()
    {
        missilePhysics.AddRelativeForce(new Vector3(0, 0, 30f));
        damage -= Time.deltaTime;
        if (damage < 0)
            Destroy(gameObject);

    }
    public void setFaction(int faction)
    {
        MeshRenderer looks = GetComponentInChildren<MeshRenderer>();
        looks.material = materials[faction];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject flashobject = GameObject.Instantiate(flash, transform.position, transform.rotation);
        Destroy(flashobject, 2);
        float radius = 2;
        float power = 200.0F;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        //Debug.Log("bum");
        foreach (Collider hit in colliders)
        {
           //Debug.Log("ała");
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 0.0F);
            destructible victim;
            try
            {
                hit.transform.parent.TryGetComponent(out victim);
                if (victim)
                {
                    Debug.Log(damage);
                    victim.TakeDmg(damage);

                }
            }
            catch { }
        }
        Destroy(gameObject);
    }

}
