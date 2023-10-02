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
    GameManager gameManager;


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


    protected override bool _canAttack
    {
        get
        {
            return (base._canAttack &&
                    timer >= timeBetweenAttacks &&
                    playerInRange &&
                    !enemyHealth.isDead
                );
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (_canAttack) Attack();
    }


    void Attack()
    {
        timer = 0f;
        if (!playerHealth.isDead) playerHealth.TakeDamage(attackDamage);
    }
}