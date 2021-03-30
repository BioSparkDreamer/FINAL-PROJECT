using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour
{
    //...................................................................Move Projectile
    public void ProjectileProperties(Vector3 shootDirection, float shootSpeed)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();

        //creates a one time push in a direction, maintains that speed
        rigidBody.AddForce(shootDirection * shootSpeed, ForceMode.Impulse);
    }
}
