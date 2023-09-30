using System;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MovementBase
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    protected override void SetInitReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        _canMove = GameManager.Instance.State == GameState.InGame;
    }

    void Update()
    {
        Move();
    }


    public void DisableMovement()
    {
        _canMove = false;
        
    }

    void Move()
    {
        if (enemyHealth.currentHealth > 0 &&
            playerHealth.currentHealth > 0)
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