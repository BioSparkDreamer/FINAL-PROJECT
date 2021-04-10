using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    //......................................Variables
    //weapons: 0Unarmed - 1Lightning - 2Fire - 3Ice
    static public float weapon;
    public int startingWeapon = 0;

    public float lightningCooldown = .8f;
    public float lightningProjectileSpeed = 20f;
    public int lightningProjectileDamage = 40;
    public GameObject lightningProjectileObject;

    public float fireCooldown = .2f;
    public float fireProjectileSpeed = 7f;
    public int fireProjectileDamage = 20;
    public GameObject fireProjectileObject;

    public float iceCooldown = 1.5f;
    public float iceProjectileSpeed = 80f;
    public int iceProjectileDamage = 50;
    public GameObject iceProjectileObject;

    public Transform fireProjectileFromLocation;
    public Transform playerPosition;

    private float shotCooldown = 0;

    Animator anim;
   
    void Start()
    {
        //weapons: 0Unarmed - 1Lightning - 2Fire - 3Ice
        weapon = startingWeapon;

        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        //......................................Cooldown
        shotCooldown = shotCooldown - Time.deltaTime;

        //......................................Shoot Projectile
        if (Input.GetButton("Fire1") && shotCooldown <= 0)
        {

            //LIGHTNING
            if(weapon == 1)
            {
                //Animation
                anim.SetInteger("State", 1);

                //spawn projectile
                GameObject newProjectile = Instantiate(lightningProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, lightningProjectileSpeed, 500f, lightningProjectileDamage, 1);
                //set cooldown
                shotCooldown = lightningCooldown;
            }

            //FIRE
            if(weapon == 2)
            {
                //Animation
                anim.SetInteger("State", 1);

                //spawn projectile
                GameObject newProjectile = Instantiate(fireProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, fireProjectileSpeed, 500f, fireProjectileDamage, 2);
                //set cooldown
                shotCooldown = fireCooldown;
            }

            //ICE
            if(weapon == 3)
            {
                //Animation
                anim.SetInteger("State", 1);

                //spawn projectile
                GameObject newProjectile = Instantiate(iceProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, iceProjectileSpeed, 500f, iceProjectileDamage, 3);
                //set cooldown
                shotCooldown = iceCooldown;
            }
        } 
    }

    //......................................Change Projectile Type
    public void ChangeElement(int element)
    {
        shotCooldown = .2f;
        weapon = element;
    }
}
