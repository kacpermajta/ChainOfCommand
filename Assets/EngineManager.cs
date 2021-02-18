using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public engineHandler[] Engines;
    public float[] conditions;
    public MapHandler HoloMap;
    public Transform holo;
//    public Transform Scaling;
    public propController propControl;
//    public float scale;
    public Material fresh, hurt, damaged;

    void Start()
    {
        conditions = new float[Engines.Length];
        for(int i = 0; i<Engines.Length; i++)
        {
            HoloMap.Entries.Add(Instantiate(HoloMap.marker, holo));
            HoloMap.localize(i, Engines[i].gameObject.transform.localPosition);
            HoloMap.setRotation(i, Engines[i].getAngle());
            conditions[i] = 1;
            //Engines[i].gameObject.transform.localRotation = Quaternion.Euler( Engines[i].getAngle());
        }

    }

    private void Update()
    {
        for (int i = 0; i < Engines.Length; i++)
        {
            if(Engines[i].condition!=conditions[i])
            {
                conditions[i] = Engines[i].condition;
                if (conditions[i] == 0)
                    HoloMap.paint(i, damaged);
                if (conditions[i] == 1)
                    HoloMap.paint(i, fresh);
                else
                    HoloMap.paint(i, hurt);

            }

        }
    }
        // Update is called once per frame

}
