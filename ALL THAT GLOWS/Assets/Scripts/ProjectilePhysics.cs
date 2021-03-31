using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour
{
    //...................................................................Variables
    public GameObject particleMissedProjectile;
    private float timeToSelfDestruct = 5f;
    private int collissionDamage;
    private int collissionElement;

    private void Update()
    {
        //...................................................................Track time this has existed
        timeToSelfDestruct = timeToSelfDestruct - Time.deltaTime;
        if (timeToSelfDestruct <= 0)
        {
            MissEffect();
        }
    }

    //...................................................................Collision
    private void OnCollisionEnter(Collision collision)
    {
        //if enemy, do damage
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthDamage>().EnemyTakeDamage(collissionDamage, collissionElement);
        }
        //play particle, remove projectile
        MissEffect();
    }

    //...................................................................Hit non-enemy or explode in air
    public void MissEffect()
    {
        //create particle
        GameObject newParticle = Instantiate(particleMissedProjectile, this.transform.position, this.transform.rotation);
        Destroy(newParticle, .5f);
        //self destruct
        Destroy(gameObject);
    }

    //...................................................................Move Projectile on creation
    public void ProjectileProperties(Vector3 shootDirection, float shootSpeed, float duration, int damage, int elementType)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();

        //creates a one time push in a direction, maintains that speed
        rigidBody.AddForce(shootDirection * shootSpeed, ForceMode.Impulse);

        //SHOULD change how long projectile lasts, currently doesn't???
        timeToSelfDestruct = duration;

        //set damage of projectile
        collissionDamage = damage;
        collissionElement = elementType;
    }
}
