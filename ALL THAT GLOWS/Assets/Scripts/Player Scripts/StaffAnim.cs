using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAnim : MonoBehaviour
{
    public GameObject staff;
    int state;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(staff.activeSelf && state != 1)
        {
            state = 1;
            anim.Play("Switch");
        }

        if (Input.GetButton("Fire1"))
        {
            anim.Play("Attack");
        }
    }
}
