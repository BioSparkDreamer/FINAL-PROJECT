using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAnim : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //anim.SetInteger("State", 1);
            anim.Play("Attack");
        }

        //anim.SetInteger("State", 0);
    }
}
