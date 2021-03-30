using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointAdjuster : MonoBehaviour
{
    //these modify the movements of enemies using these waypoints
    public float aproachSpeed = 5;
    public float waitThenCome = 0;
    

    private void Start()
    {
        //disable the sphere model used to help position waypoints
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}
