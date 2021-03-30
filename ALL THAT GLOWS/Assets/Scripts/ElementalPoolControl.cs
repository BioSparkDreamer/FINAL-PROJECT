using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPoolControl : MonoBehaviour
{
    public int element1_Lightning2_Fire3_Ice = 1;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerWeaponFire>().ChangeElement(element1_Lightning2_Fire3_Ice);
        }
    }
}
