using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentInput : MonoBehaviour
{

    public float right, left, up, down, clockwise, counterclockwise, throttle, halt; 
    public bool dampen, shoot, altshoot;
    public int formation;

    public bool looking;

    public bool isPlayer, isleader;

    public Transform Target, Mothership, shootTarget, hostileTarget, personalWaypoint;

    public int faction;

    public LeaderScript leaderController;
    public float TargetAlignment;
    public float TargetAlignmentA;
    public int machines;
    public int holos;

    public propController[] props;

    public mode playerMode;
    public mode GUIMode;


    public enum mode { lookaround, steer, enginePrev, targeting}

    // Start is called before the first frame update
    void Start()
    {
        formation = 1;
        personalWaypoint = GameObject.Instantiate(new GameObject(), transform.position, transform.rotation).transform;
    }

    // Update is called once per frame
    void Update()
    {
        up = 0;
        down = 0;
        left = 0;
        right = 0;
        clockwise = 0;
        counterclockwise = 0;
        throttle = 0;
        halt = 0;

        if (isPlayer)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GameController.ShipList = new List<ShipEntity>();
                PersistantValues.inBattle = false;
                SceneManager.LoadScene("chooseModel");

            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                looking = true;
            }
            else
                looking = false;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                shoot = true;
            }
            else
                shoot = false;

            if (Input.GetKey(KeyCode.Alpha1))
            {
                if (machines!=1)
                {
                    machines = 1;
                    props[machines].activate();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (holos != 2)
                {
                    holos = 2;
                    props[holos].activate();
                }
                else
                {
                    holos = 0;
                    GUIMode = mode.lookaround;
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            { 
                if (holos != 3)
                {
                    holos = 3;
                    props[holos].activate();
                }
                else
                {
                    holos = 0;
                    GUIMode = mode.lookaround;
                }
            }
            /*
            if (playerMode == mode.enginePrev)
            {

                if (looking)
                {
                    machines = 0;
                    playerMode = mode.lookaround;
                }
            }*/

            if (playerMode == mode.steer)
            {
                float deadzone = 0.1f;
                if (looking)
                {
                    machines = 0;
                    playerMode = mode.lookaround;
                }

                float aboveCenter = Mathf.Min(Screen.height, Mathf.Max(0, Input.mousePosition.y)) - (Screen.height / 2);
                float rightCenter = Mathf.Min(Screen.width, Mathf.Max(0, Input.mousePosition.x)) - (Screen.width / 2);
                if (aboveCenter> Screen.height* deadzone)
                {
                    up = (aboveCenter- Screen.height * deadzone) / ((Screen.height*(1-2*deadzone)) / 2);
                }
                if (aboveCenter < -Screen.height * deadzone)
                {
                    down = -(aboveCenter + Screen.height * deadzone) / ((Screen.height * (1 - 2 * deadzone)) / 2);
                }

                if (rightCenter > Screen.width* deadzone)
                {
                    right = (rightCenter - Screen.width * deadzone) / ((Screen.width * (1 - 2 * deadzone)) / 2);
                }
                if (rightCenter < -Screen.width * deadzone)
                {
                    left = -(rightCenter + Screen.width * deadzone) / ((Screen.width * (1 - 2 * deadzone)) / 2);
                }


            }


            if (Target ?? false)
                TargetAlignment = Vector3.Distance(Target.position, Mothership.position);
            else
                TargetAlignment = 0;


            if (Input.GetKey(KeyCode.F1))
            {
                formation = 1;
            }
            if (Input.GetKey(KeyCode.F2))
            {
                formation = 2;
            }
            if (Input.GetKey(KeyCode.F3))
            {
                formation = 3;
            }
            if (Input.GetKey(KeyCode.F4))
            {
                formation = 4;
            }



            if (Input.GetKey(KeyCode.S))
            {
                down = 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                up = 1;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                throttle = 1;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                dampen = false;
            }
            else
                dampen = true;

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                halt = 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                left = 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                right = 1;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                counterclockwise = 1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                clockwise = 1;
            }

        }
        else 
        {
            shoot = false;

            dampen = true;

            if (Target != null)
            {




                float odleglosc = Vector3.Distance(Target.position, Mothership.position);
                TargetAlignment = odleglosc;
                if (odleglosc > 3)
                {
                    Vector3 toTarget = (Target.position - Mothership.position).normalized;

                    if (Vector3.Dot(toTarget, Mothership.forward) > 0)
                    {


                        //jeśli przed-obrót i gaz
                        Vector3 targetLocation = Mothership.transform.InverseTransformPoint(Target.position);

                        //Debug.Log(targetLocation.normalized);

                        if (targetLocation.normalized.x < -0.05)
                            left = 1;
                        if (targetLocation.normalized.x > 0.05)
                            right = 1;
                        if (targetLocation.normalized.y > 0.05)
                            up = 1;
                        if (targetLocation.normalized.y < -0.05)
                            down = 1;

                        if (Mathf.Abs(targetLocation.normalized.y) < 0.6 && Mathf.Abs(targetLocation.normalized.x) < 0.6)
                        {

                            if(!isleader || leaderController.CheckAlignment())
                                throttle = 1;

                        }
                        else
                            dampen = false;
                        AdjustDirection(true);






                    }
                    else
                    {
                        TargetAlignment += 1000;
                        if (odleglosc < 12)
                        {
                            //jeśli za i blisko-na wstecznym
                            


                            Vector3 targetLocation = Mothership.transform.InverseTransformPoint(Target.position);
                            if (Mathf.Abs(targetLocation.normalized.y) < 0.8 && Mathf.Abs(targetLocation.normalized.x) < 0.8)
                                halt = 1;
                            else
                                dampen = false;
                            //Debug.Log(targetLocation.normalized);

                            if (targetLocation.normalized.x < -0.1)
                                right = 1;
                            if (targetLocation.normalized.x > 0.1)
                                left = 1;
                            if (targetLocation.normalized.y > 0.1)
                                down = 1;
                            if (targetLocation.normalized.y < -0.1)
                                up = 1;


                        }
                        else
                        {
                            //jeśli za i daleko - obrót
                            Vector3 targetLocation = Mothership.transform.InverseTransformPoint(Target.position);

                            //Debug.Log(targetLocation.normalized);

                            if (targetLocation.normalized.x < 0)
                                left = 1;
                            if (targetLocation.normalized.x >= 0)
                                right = 1;
                            if (targetLocation.normalized.y > 0)
                                up = 1;
                            if (targetLocation.normalized.y <= 0)
                                down = 1;



                        }
                    }
                }
                else
                {
                    Quaternion rozniceObrotuQ = Quaternion.FromToRotation(Mothership.forward, Target.forward);
                    Vector3 rozniceObrotu = Mothership.transform.InverseTransformVector(rozniceObrotuQ.eulerAngles);


                    Vector3 rozniceVektorFt = Mothership.InverseTransformVector(Target.forward);
                    Vector3 rozniceVektorUt = Mothership.InverseTransformVector(Target.up);
                    Vector3 rozniceVektorRt = Mothership.InverseTransformVector(Target.right);
                    Vector3 rozniceVektorF = rozniceVektorFt / rozniceVektorFt.magnitude;
                    Vector3 rozniceVektorU = rozniceVektorUt / rozniceVektorUt.magnitude;
                    Vector3 rozniceVektorR = rozniceVektorRt / rozniceVektorRt.magnitude;


                    //Debug.Log(rozniceVektorF + " | "+ rozniceVektorU + " | "+ rozniceVektorR + " | "+ rozniceVektorFt.magnitude + " | "+ rozniceVektorUt.magnitude + " | " + rozniceVektorRt.magnitude);

                    if (rozniceVektorF.z < 0.95 && rozniceVektorU.z < 0)
                        up = 1;
                    if (rozniceVektorF.z < 0.95 && rozniceVektorU.z > 0)
                        down = 1;
                    if (rozniceVektorF.z < 0.95 && rozniceVektorR.z > 0)
                        left = 1;
                    if (rozniceVektorF.z < 0.95 && rozniceVektorR.z < 0)
                        right = 1;
                    if (rozniceVektorU.y < 0.95 && rozniceVektorR.y > 0)
                        counterclockwise = 1;
                    if (rozniceVektorU.y < 0.95 && rozniceVektorR.y < 0)
                        clockwise = 1;

                    if(rozniceVektorF.z<0)
                    {
                        TargetAlignment += 1000;
                    }

                    //jeśli na miejscu ale zły obrót-obrót

                    //jeśli na miejscu i dobry obrót-stoi
                    //jesli podazalismy za personalwaypoint to teraz go zdejmujemy
                    if (Target == personalWaypoint)
                        Target = null;

                   
                }
                if(Target == shootTarget && odleglosc<80)
                {
                    personalWaypoint.transform.position = transform.TransformPoint(new Vector3(460, 0, 160));
                    Target = personalWaypoint;
                    //ustaw personalwaypoint
                }




            }
            else
            {
                if (shootTarget ??false && Vector3.Distance(shootTarget.position, Mothership.position) < 1500)
                    Target = shootTarget;
                //czyli gdy jesteśmy dowódcą bez przydziału
                //sprawdzamy shootingtarget, i jak jest daleko to ustawiamy na niego target
                //jeśli shooting target jest blisko, to wyliczamy waypoint i ustawiamy tam personalwaypoint i ustawiamy na niego target

            }
            if(shootTarget ==null || Vector3.Distance(shootTarget.position, Mothership.position)>1500)
            {
                float currentdistance = 15000000;
                float newdistance;
                if (!isleader)
                {
                    currentdistance = 1500;
                    //shootTarget = null;
                }
                foreach(ShipEntity potentialTarget in GameController.ShipList)
                {
                    newdistance = Vector3.Distance(potentialTarget.Model.transform.position, Mothership.position) ;
                    if (potentialTarget.faction != faction && newdistance < currentdistance)
                    {
                        shootTarget = potentialTarget.Model.transform;
                        currentdistance = newdistance;
                    }

                }
                if (isleader && (shootTarget ?? false))
                {
                    leaderController.assignTarget(shootTarget);

                }

            }
            if (shootTarget ?? false)
            {
                Vector3 toShootTarget = (shootTarget.position - Mothership.position);
                //Debug.Log(Vector3.Angle(toShootTarget, Mothership.forward));
                if (Vector3.Angle(toShootTarget, Mothership.forward) < 5 && Vector3.Distance(shootTarget.position, Mothership.position) <1500)
                    shoot = true;

            }
        }
    }

    public void AdjustDirection (bool flying)
    {
        if ((shootTarget ?? false) && (Target ?? false))
        {
            Vector3 toShootTarget = (shootTarget.position - Mothership.position);
            Vector3 toTarget = (Target.position - Mothership.position);

            //Debug.Log(Vector3.Angle(toShootTarget, Mothership.forward));
            if (Vector3.Angle(toShootTarget, Mothership.forward) < 30 && (!flying || Vector3.Angle(toTarget, Mothership.forward) < 30))
            {
                left = 0;
                right = 0;
                up = 0;
                down = 0;

                Vector3 targetLocation = Mothership.transform.InverseTransformPoint(shootTarget.position);


                if (targetLocation.normalized.x < -0.05)
                    left = 1;
                if (targetLocation.normalized.x > 0.05)
                    right = 1;
                if (targetLocation.normalized.y > 0.05)
                    up = 1;
                if (targetLocation.normalized.y < -0.05)
                    down = 1;

            }
        }

    }
}
