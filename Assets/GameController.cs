using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameObject victoryScreen;
    public GameObject victoryScreenAssign;
    public enum shiptype {AI, AI_lead, player_lead, player  };

    int[] shipcounter= new int[3];
    public Vector3[] startingLocation=new Vector3[3];
    public static GameController masterController;

    public static List<ShipEntity> ShipList= new List<ShipEntity>();
    // Start is called before the first frame update
    public GameObject[] entrypoints;

    public GameObject[] ShipModels = new GameObject[3];
    public GameObject[] playerChar;
    public GameObject[] LeaderShips;




    void Awake()
    {
        PersistantValues.inBattle = true;
        masterController = this;
        shipcounter[0] = -1;
        shipcounter[1] = -1;
        shipcounter[2] = -1;
    }
    private void Start()
    {

        victoryScreen = victoryScreenAssign;
        
        Spawnship(1, playerChar[PersistantValues.shipModel]);
        
        Spawnship(2, LeaderShips[0]);
        Spawnship(1, LeaderShips[0]);
        Spawnship(2, LeaderShips[0]);
        Spawnship(1, LeaderShips[0]);
        Spawnship(2, LeaderShips[0]);
        Spawnship(1, LeaderShips[0]);
        Spawnship(2, LeaderShips[0]);
        Spawnship(1, LeaderShips[0]);
        Spawnship(2, LeaderShips[0]);
        Spawnship(1, LeaderShips[0]);
        Spawnship(2, LeaderShips[0]); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject Spawnship(int Faction, int Model, shiptype type)
    {
        GameObject newModel;
        switch (type)
        {
            case shiptype.AI:
                //newModel = 
                break;
            case shiptype.player_lead:
                break;
            case shiptype.AI_lead:
                break;
        }
        
        return Spawnship(Faction, ShipModels[Model]);
    }
    public GameObject Spawnship(int Faction, GameObject Model)
    {
        GameObject Spawned = GameObject.Instantiate(Model, startingLocation[Faction] + new Vector3(shipcounter[Faction] * 50, 0), Quaternion.identity);
        shipcounter[Faction] += 1;

        ShipList.Add(new ShipEntity(Spawned, Faction));
        Spawned.GetComponent<shipDrive>().shipController.faction = Faction;

        return Spawned;
    }
    public static void RemoveFromList(GameObject removed)
    {
        int numberHostiles=0;
        ShipEntity removedEntity=null;
        foreach(ShipEntity entity in ShipList)
        {
            //Debug.Log("ded?????"+ ShipList.Count);

            
            if(entity.Model == removed)
            {
                //Debug.Log("ded");
                if (entity.Model.GetComponent<shipDrive>().shipController.isPlayer)
                {
                    //Debug.Log("plr ded");
                    GameController.ShipList = new List<ShipEntity>();
                    PersistantValues.inBattle = false;
                    SceneManager.LoadScene("gameOver");
                }
                //Debug.Log("ded" + ShipList.Count);
                removedEntity = entity;
                //ShipList.Remove(entity);


            }
            else if (entity.faction == 2)
                numberHostiles++;
        }
        if (removedEntity != null)
        {
            ShipList.Remove(removedEntity);
            //Debug.Log("ded!" + ShipList.Count);
        }
        //Debug.Log("wrogowie:" + numberHostiles);
        if (numberHostiles==0)
        {
            //Debug.Log("YAY!");
            victoryScreen.SetActive(true);
        }


    }

}



public class ShipEntity
{
    public GameObject Model;
    public int faction;

    public ShipEntity(GameObject model, int faction)
    {
        Model = model;
        this.faction = faction;
    }
}
