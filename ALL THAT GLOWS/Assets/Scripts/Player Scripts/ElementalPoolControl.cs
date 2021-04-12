using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPoolControl : MonoBehaviour
{
    [Header("What element to switch a player who touches this to")]
    [Tooltip("0=nothing, 1=lightning, 2=fire, 3=ice")]
    public int element1_Lightning2_Fire3_Ice = 1;

    [Header("This script also controls the staff models (put the models here)")]
    public GameObject fire;
    public GameObject ice;
    public GameObject lightning;
    

    void Start()
    {
        //NOTE FROM MIKE: Removed these because they were keeping us from disabling the staffs on start
        //fire = GameObject.FindGameObjectWithTag("Fire Staff");
        //ice = GameObject.FindGameObjectWithTag("Ice Staff");
        //lightning = GameObject.FindGameObjectWithTag("Lightning Staff");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Change weapon damage type on PLayerWeaponFire script
            other.GetComponent<PlayerWeaponFire>().ChangeElement(element1_Lightning2_Fire3_Ice);

            //Conditionals to hide the other staffs when picking up one
            if (element1_Lightning2_Fire3_Ice == 1)
            {
                lightning.SetActive(true);
                fire.SetActive(false);
                ice.SetActive(false);
            }
            else if (element1_Lightning2_Fire3_Ice == 2)
            {
                lightning.SetActive(false);
                fire.SetActive(true);
                ice.SetActive(false);
            }
            else if (element1_Lightning2_Fire3_Ice == 3)
            {
                lightning.SetActive(false);
                fire.SetActive(false);
                ice.SetActive(true);
            }
        }
    }
}
