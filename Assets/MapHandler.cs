using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public List<GameObject> Entries;
    public bool DisplayShips;
    public GameObject marker;
    public shipDrive thisVessel;
    public Color enemy, friend, behind;
    public float scale;
    public bool frontal;
    public Transform referencePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DisplayShips&&PersistantValues.inBattle)
        {
            int braki = GameController.ShipList.Count - Entries.Count;
            if (braki>0)
            {
                for (int i = 0; i < braki; i++)
                    Entries.Add(Instantiate(marker,transform));
            }
            if(braki<0)
            {
                for (int i = 0; i < -braki; i++)
                    Entries.Remove(Entries[i]);
            }
            for(int i = 0; i<Entries.Count; i++)
            {
                Vector3 tempVector = thisVessel.transform.InverseTransformPoint(GameController.ShipList[i].Model.transform.position) / scale;

                if (tempVector.magnitude > 0.4)
                    tempVector = (tempVector / tempVector.magnitude) * 0.4f;

                Entries[i].transform.localPosition = new Vector3(tempVector.x, tempVector.z, -tempVector.y);
                if (frontal && tempVector.z < 0)
                    colorize(i, behind);
                else
                {
                    if (GameController.ShipList[i].faction != thisVessel.shipController.faction)
                        colorize(i, enemy);
                    else
                        colorize(i, friend);
                }

            }
        }
    }
    public void localize(int entry, Vector3 location)
    {
        Entries[entry].transform.localPosition = location;
    }
    public void setRotation(int entry, Vector3 rotation)
    {
        Entries[entry].transform.localRotation = Quaternion.Euler( rotation);
    }
    public void colorize(int entry, Color value)
    {
        Entries[entry].GetComponent<SpriteRenderer>().color = value;
    }
    public void paint(int entry, Material value)
    {
        Entries[entry].GetComponentInChildren<MeshRenderer>().material = value;
    }

}
