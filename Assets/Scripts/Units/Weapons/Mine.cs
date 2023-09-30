using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : AttackerBase
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float explosionRadius = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            Explode(other.gameObject);
        }
    }



    void Explode(GameObject enemy)
    {
        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth == null)
            return;
        
        // Add explosion force and damage nearby 
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            // if (rb != null)
            // {
            //     rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            // }

            EnemyHealth eHealth = nearbyObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                eHealth.TakeDamage(damage, gameObject.transform.localPosition);
            }
        }
        
        Destroy(gameObject);
    }
}
