using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {

    //explosivity multiplier determines the size of the explosion radius
    public float explosivityMultiplier = 0.03f;
    public float maxExplosionRadius = 5f;

    private Bullet theProjectile;

    private void Start()
    {
        theProjectile = GetComponent<Bullet>();
    }

    public void Explosion()
    {
        //gets damage of exploding damage when hitting target
        int bulletDamage = theProjectile.damage;

        //the max damage potential of the explosion must not exceed the original damage value of the projectile
        int maxDamagePotential = bulletDamage;
        //the explosion radius is caluclated based on the explosivity multiplier and the maxDamagePotential
        //explosion radius has maximum otherwise player is likely to receive damage from explosion
        float explosionRadius = Mathf.Clamp(explosivityMultiplier * maxDamagePotential, 0, maxExplosionRadius);
        
        Debug.Log(explosionRadius);
        //all gameobjects with colliders such as enemy tanks that are within the radius of the explosion are stored in
        //an array
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        //goes through each gameobject in the array affected by the explosion
        foreach(Collider2D collider in hitColliders)
        {
            //distance between source of explosion and current gameobject is calculated
            float dist = Vector3.Distance(transform.position, collider.transform.position);
            //damage inflicted to gameobject is determined by multiplying max damage potential by distance divided by explosion radius
            int damageInflicted = Mathf.RoundToInt(maxDamagePotential * (dist / explosionRadius));
            //if gameobject can take damage then reduce health accordingly
            collider.transform.SendMessage("TakeDamage", damageInflicted, SendMessageOptions.DontRequireReceiver);
        }
    }
}
