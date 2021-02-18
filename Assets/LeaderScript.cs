using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderScript : MonoBehaviour
{
    int formationNum=-1;

    public List<Station> Unit =new List<Station>();

    public MapHandler formationMap;
    public AgentInput shipController;
    public GameObject[] Waypoints;



    public Vector3[] Formation1 = new Vector3[] { new Vector3(0, -10, -10), new Vector3(-10, 0, -10), new Vector3(10, 0, -10), new Vector3(0, 10, -10) };
    public Vector3[] Formation2 = new Vector3[] { new Vector3(10, 0, -10), new Vector3(-10, 0, -10), new Vector3(20, 0, -15), new Vector3(-20, 0, -15) };
    public Vector3[] Formation3 = new Vector3[] { new Vector3(10, 0, -10), new Vector3(-10, 0, -10), new Vector3(10, 0, -20), new Vector3(-10, 0, -20) };
    public Vector3[] Formation4 = new Vector3[] { new Vector3(10, 0, -20), new Vector3(-10, 0, -20), new Vector3(20, 0, -10), new Vector3(-20, 0, -10) };
    public Vector3[] CurrentFormation;

    public Vector3[][] AllFormations = new Vector3[][] {
        new Vector3[] {new Vector3(0,0,0) },
        new Vector3[] { new Vector3(0, -10, -10), new Vector3(-10, 0, -10), new Vector3(10, 0, -10), new Vector3(0, 10, -10) },
        new Vector3[] { new Vector3(10, 0, -10), new Vector3(-10, 0, -10), new Vector3(20, 0, -15), new Vector3(-20, 0, -15) },
        new Vector3[] { new Vector3(10, 0, -10), new Vector3(-10, 0, -10), new Vector3(10, 0, -20), new Vector3(-10, 0, -20) },
        new Vector3[] { new Vector3(10, 0, -20), new Vector3(-10, 0, -20), new Vector3(20, 0, -10), new Vector3(-20, 0, -10) } };

    // Start is called before the first frame update
    private void Awake()
    {
        shipController.leaderController = this;
        shipController.isleader = true;
    }
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            formationMap.colorize(i, Color.yellow);
        }
        formationMap.colorize(4, Color.magenta);

        
        GameObject Newship;
        /*
        Newship = GameController.masterController.Spawnship(shipController.faction, 0);
        Unit.Add(new Station(Waypoints[0], Newship.GetComponent<shipDrive>()));

        //Newship.GetComponent<shipDrive>().shipController.Target = Waypoints[0].transform;
        
        Unit[0].designation.shipController.Target = Unit[0].Waypoint.transform;

        //Unit.Add(new Station())
        Newship = GameController.masterController.Spawnship(shipController.faction, 0);
        Unit.Add(new Station(Waypoints[1], Newship.GetComponent<shipDrive>()));
        //Newship.GetComponent<shipDrive>().shipController.Target = Waypoints[1].transform;

        Newship = GameController.masterController.Spawnship(shipController.faction, 0);
        Unit.Add(new Station(Waypoints[2], Newship.GetComponent<shipDrive>()));
        //Newship.GetComponent<shipDrive>().shipController.Target = Waypoints[2].transform;

        Newship = GameController.masterController.Spawnship(shipController.faction, 0);
        Unit.Add(new Station(Waypoints[3], Newship.GetComponent<shipDrive>()));
        //Newship.GetComponent<shipDrive>().shipController.Target = Waypoints[3].transform;
        */

        
        for(int i=0; i<4; i++)
        {
            Newship = GameController.masterController.Spawnship(shipController.faction, 0,GameController.shiptype.AI);
            Unit.Add(new Station(Waypoints[i], Newship.GetComponent<shipDrive>()));
            Unit[i].designation.shipController.Target = Unit[i].Waypoint.transform;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if(shipController.formation!=formationNum)
        {
            formationNum = shipController.formation;
            CurrentFormation = AllFormations[formationNum];
            for (int i=0; i<4;  i++)
            {
                formationMap.localize(i, new Vector3(CurrentFormation[i].x / 50, CurrentFormation[i].z / 50, -0.1f));

                Waypoints[i].transform.localPosition = 2*CurrentFormation[i];

            }
        }
    }
    public void assignTarget(Transform Target)
    {
        foreach (Station subordinate in Unit)
            subordinate.designation.shipController.shootTarget = Target; 
    }
    public bool CheckAlignment()
    {
        float totaldistance = 0;
        foreach(Station subordinate in Unit )
        {
            totaldistance += subordinate.designation.shipController.TargetAlignment;
        }
        if (totaldistance/Unit.Count > 20)
            return false;
        return true;

    }

}
public class Station
{
    //Vector3 WaypointPosition;
    public GameObject Waypoint;
    public shipDrive designation;

    public Station(GameObject waypoint, shipDrive designation)
    {
        //WaypointPosition = waypointPosition;
        Waypoint = waypoint;
        this.designation = designation;
    }
}