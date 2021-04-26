using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayerScript : MonoBehaviour
{
    //................................................Call function from player script to force the player to jump on collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().Jump();
        }
    }
}
