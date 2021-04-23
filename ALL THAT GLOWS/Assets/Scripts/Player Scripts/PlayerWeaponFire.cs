using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    //......................................Variables
    //weapons: 0Unarmed - 1Lightning - 2Fire - 3Ice
    private int weapon;
    [Header("Default starting weapon")]
    [Tooltip("0=nothing, 1=lightning, 2=fire, 3=ice")]
    public int startingWeapon = 0;

    [Header("Lightning Weapon variables")]
    public float lightningCooldown = .8f;
    public float lightningProjectileSpeed = 20f;
    public int lightningProjectileDamage = 40;
    [Tooltip("Put the projectile prefab here")]
    public GameObject lightningProjectileObject;

    [Header("Fire weapon variables")]
    public float fireCooldown = .2f;
    public float fireProjectileSpeed = 7f;
    public int fireProjectileDamage = 20;
    [Tooltip("Put the projectile prefab here")]
    public GameObject fireProjectileObject;

    [Header("Ice weapon variables")]
    public float iceCooldown = 1.5f;
    public float iceProjectileSpeed = 80f;
    public int iceProjectileDamage = 50;
    [Tooltip("Put the projectile prefab here")]
    public GameObject iceProjectileObject;

    [Header("The position projectiles will be fired from (an empty gameobject in the player)")]
    public Transform fireProjectileFromLocation;
    [Header("The player gameobject")]
    public Transform playerPosition;

    private float shotCooldown = 0;

    Animator anim;

    [Header("Audio Sources")]
    public AudioSource electroballAudio;
    public AudioSource fireballAudio;
    public AudioSource iceballAudio;
    public AudioSource changeBallStateAudio;
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
                //spawn projectile
                GameObject newProjectile = Instantiate(lightningProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, lightningProjectileSpeed, 500f, lightningProjectileDamage, 1);
                //set cooldown
                shotCooldown = lightningCooldown;

                //Animation
                //anim.SetInteger("State", 1);

                electroballAudio.PlayOneShot(electroballAudio.clip);
            }

            //FIRE
            if(weapon == 2)
            {
                //spawn projectile
                GameObject newProjectile = Instantiate(fireProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, fireProjectileSpeed, 500f, fireProjectileDamage, 2);
                //set cooldown
                shotCooldown = fireCooldown;

                //Animation
                //anim.SetInteger("State", 1);

                fireballAudio.PlayOneShot(fireballAudio.clip);            
            }

            //ICE
            if (weapon == 3)
            {
                //spawn projectile
                GameObject newProjectile = Instantiate(iceProjectileObject, fireProjectileFromLocation.position, fireProjectileFromLocation.rotation);

                //calculate shoot angle
                Vector3 shootDirection = (fireProjectileFromLocation.position - playerPosition.position);
                //send projectile off
                newProjectile.GetComponent<ProjectilePhysics>().ProjectileProperties(shootDirection, iceProjectileSpeed, 500f, iceProjectileDamage, 3);
                //set cooldown
                shotCooldown = iceCooldown;

                //Animation
                //anim.SetInteger("State", 1);

                iceballAudio.PlayOneShot(iceballAudio.clip);
            }
        } 
    }

    //......................................Change Projectile Type
    public void ChangeElement(int element)
    {
        if (weapon != element)
        {
            changeBallStateAudio.Play();
        }
        shotCooldown = .2f;
        weapon = element;
    }
}

    //void PlayAudioOnGameobject(GameObject go)
    //{
    //    var audio = go.GetComponent<AudioSource>();
    //    if (audio != null)
    //    {
    //        audio.Play();
    //    }
    //}

