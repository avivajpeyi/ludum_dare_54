using System;
using UnityEngine;
using System.Collections;

public class EnemyAttack : AttackerBase
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    bool animPresent = false;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    private void Start()
    {
        _canAttack = GameManager.Instance.State == GameState.InGame; 
    }

    public override void SetInitReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        if (anim != null)
            animPresent = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (
            base._canAttack &&
            timer >= timeBetweenAttacks &&
            playerInRange &&
            enemyHealth.currentHealth > 0
        )
        {
            Attack();
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}