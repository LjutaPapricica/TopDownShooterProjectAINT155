using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {
    
    public float explosivityMultiplier = 0.03f;

    private Bullet theBullet;

    private void Start()
    {
        theBullet = GetComponent<Bullet>();
    }

    public void Explosion()
    {
        int bulletDamage = theBullet.damage;

        int maxDamagePotential = bulletDamage;
        float explosionRadius = explosivityMultiplier * bulletDamage;

        Debug.Log(explosionRadius.ToString());

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D collider in hitColliders)
        {
            float dist = Vector3.Distance(transform.position, collider.transform.position);
            int damageInflicted = Mathf.RoundToInt(explosionRadius * (1 / dist));
            collider.transform.SendMessage("TakeDamage", damageInflicted, SendMessageOptions.DontRequireReceiver);
        }
    }
}
