using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    //......................................Variables
    //weapons: 0Unarmed - 1Lightning - 2Fire - 3Ice
    static public float weapon;
    public int startingWeapon = 0;

    public GameObject projectileObject;
    public Transform fireProjectileFromLocation;
    public Transform playerPosition;

    private float shotCooldown = 0;


   
    void Start()
    {
        //weapons: 0Unarmed - 1Lightning - 2Fire - 3Ice
        weapon = startingWeapon;
        
    }

  
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && shotCooldown <= 0)
        {
            //spawn projectile
            GameObject newProjectile = Instantiate(projectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

            //calculate shoot angle
            Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
            //send projectile off
            newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, 5);
            Destroy(newProjectile, 5f);

            //LIGHTNING
            if(weapon == 1)
            {

            }

            //FIRE
            if(weapon == 2)
            {

            }

            //ICE
            if(weapon == 3)
            {

            }


        }
    }
}
