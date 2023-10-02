using System;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MovementBase
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    GameManager gameManager;


    protected override void SetInitReferences()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player = playerHealth.transform;
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
}