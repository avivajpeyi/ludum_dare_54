using System;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MovementBase
{
    Transform player;
    PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;
    GameManager gameManager;
    
    [SerializeField] private float baseSpeed = 3.5f;
    
    public void SetSpeed(float speed)
    {
        nav.speed = speed;
    }
    


    protected override void SetInitReferences()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player = playerHealth.transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetSpeed(baseSpeed);
    }


    void Update() => Move();

    


    void Move()
    {
        if (_canMove && !playerHealth.isDead)
        {
            nav.isStopped = false;
            nav.SetDestination(player.position);
        }
        else
        {
            nav.isStopped = true;
            // turn off anim
        }
    }

    public void DisableMovement()
    {
        // Delete navmeshagent
        Destroy(nav);
        this.enabled = false;
    }
}