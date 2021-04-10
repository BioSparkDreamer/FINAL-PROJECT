using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPoolControl : MonoBehaviour
{
    public int element1_Lightning2_Fire3_Ice = 1;

    public GameObject fire;
    public GameObject ice;
    public GameObject lightning;

    void Start()
    {
        fire = GameObject.FindGameObjectWithTag("Fire Staff");
        ice = GameObject.FindGameObjectWithTag("Ice Staff");
        lightning = GameObject.FindGameObjectWithTag("Lightning Staff");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
