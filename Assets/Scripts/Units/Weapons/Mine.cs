using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float explosionRadius = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


    private void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            Explode(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        
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
