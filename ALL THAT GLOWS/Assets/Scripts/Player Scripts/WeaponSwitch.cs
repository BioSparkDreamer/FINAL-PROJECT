using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("This script also controls the staff models (put the models here)")]
    public GameObject fire;
    public GameObject ice;
    public GameObject lightning;

    public GameObject elementChangerObject;
    private PlayerWeaponFire elementChanger;

    void Start()
    {
        elementChanger = elementChangerObject.GetComponent<PlayerWeaponFire>();
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            lightning.SetActive(true);
            fire.SetActive(false);
            ice.SetActive(false);

            elementChanger.ChangeElement(1);
        }

        if (Input.GetKeyDown("2"))
        {
            lightning.SetActive(false);
            fire.SetActive(true);
            ice.SetActive(false);

            elementChanger.ChangeElement(2);
        }

        if (Input.GetKeyDown("3"))
        {
            lightning.SetActive(false);
            fire.SetActive(false);
            ice.SetActive(true);

            elementChanger.ChangeElement(3);
        }
    }
}
