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

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _canMove = gameManager.State == GameState.InGame;
    }

    void Update() => Move();


    public void DisableMovement() => _canMove = false;


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