using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMoveToWaypoints : MonoBehaviour

{
    //.............................................Waypoint Variables
    [Header("Set max number of waypoints - spawner can't add more than the number here (default 25)")]
    public Transform[] waypoint;
    int waypointIndex = 0;
    private WaypointAdjuster waypointAdjuster;

    private GameObject[] newRoute;

    private float moveSpeed;
    private float waitTime = 0;

    //.............................................Movement Variables
    [Header("Move speed and turn speed while moving")]
    public float defaultMoveSpeed = 5f;
    public bool startMovement = true;
    private Vector3 waypointLocationCheck;
    public float rotateSpeed = 2f;

    //.............................................Animation
    public Animator anim;



    void Start()
    {
        //.............................................Instantiation
        anim = gameObject.GetComponent<Animator>();

        moveSpeed = defaultMoveSpeed;
        //checks first waypoint in array for a
        //script, adds its modifications if applicable
        CheckWaypoint();
    }

    void Update()
    {
        //.............................................Turn on movement
        if (startMovement == true)
        {
            //check if current destination reached on x/z axis
            waypointLocationCheck = waypoint[waypointIndex].transform.position;
            waypointLocationCheck.y = transform.position.y;
            if (Vector3.Distance(waypointLocationCheck, transform.position) < 1)
            {
                //.............................................Set next waypoint in array as new target
                if (waypointIndex < waypoint.Length - 1)
                {
                    waypointIndex = waypointIndex + 1;
                    //check if it has a WaypointAdjuster script
                    //CheckWaypoint(); (NOT USING!)
                }
                //...............................................Turn off movement at end of array
                else
                {
                    waypointIndex = 0;
                    startMovement = false;
                }
            }

            anim.SetBool("IsMoving", true);
            MoveToWaypoint();
            RotateTowardsLocation(waypoint[waypointIndex].transform.position);

        }

        else
        {

            anim.SetBool("IsMoving", false);
        }
    }

    void CheckWaypoint()
    {
        //check if waypoint has a WaypointAdjuster script
        GameObject waypointScriptCheck = waypoint[waypointIndex].gameObject;
        waypointAdjuster = waypointScriptCheck.GetComponent<WaypointAdjuster>();

        //If it does, grab its values to change how
        //the enemy will move
        if (waypointScriptCheck != null)
        {
            moveSpeed = waypointAdjuster.aproachSpeed;
            waitTime = waypointAdjuster.waitThenCome;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            waitTime = 0f;
        }
    }

    void MoveToWaypoint()
    {
        //Pause movement if there is a wait time
        if (waitTime > 0f)
        {
            waitTime = waitTime - Time.deltaTime;
            return;
        }
        else
        {
            //Move toward waypoint and animation declaration
            transform.position = Vector3.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsLocation(Vector3 patrolLocation)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = patrolLocation - transform.position;

        // remove verticality
        targetDirection.y = 0;

        // The step size is equal to speed times frame time.
        float singleStep = rotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }


    //...............................................When spawned, take movement orders
    public void OnCreationWaypoints(GameObject route)
    {
        if (route != null)
        {
            
            newRoute = route.GetComponent<EnemyMovementPath>().wayPoints;
            //System.Array.Copy(newRoute, route.GetComponent<EnemyMovementPath>().wayPoints, 1);

            print("ENEMY SPAWN: Route Detected, changing to waypoints in " + route.name + ". Adding " + newRoute.Length + " waypoints.");

            //change passed array into local array
            for (int i = 0; i < newRoute.Length; i++)
            {
                waypoint[i] = newRoute[i].transform;
            }
        }
        else
        {
            print("ENEMY SPAWN: Error! No route detected, using default movement");
        }
    }

}
