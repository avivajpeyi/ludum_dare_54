using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

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
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage, gameObject.transform.localPosition);
            Destroy(gameObject);
        }
    }
}
