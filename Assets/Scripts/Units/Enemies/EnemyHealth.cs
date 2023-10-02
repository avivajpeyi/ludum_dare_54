using System;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class EnemyHealth : HealthBase
{
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public static event Action OnEnemyDeath;


    bool isSinking;

    
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    protected override void Death()
    {
        base.Death();
        ScoreManager.Instance.UpdateScore(scoreValue);
        PlayerLevel.Instance.IncreaseXP(scoreValue);
        StartSinking();
        foreach (Collider c in GetComponents<Collider>())
        {
            Destroy(c);
        }

        OnEnemyDeath?.Invoke();
        
    }


    public void StartSinking()
    {
        GetComponent<EnemyMovement>().DisableMovement();
        
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}