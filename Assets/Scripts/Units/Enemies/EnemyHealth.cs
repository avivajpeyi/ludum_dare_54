using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100f;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    GameManager gameManager;

    [SerializeField] [Tooltip("The CAPSULE hitbox collider of the enemy")]
    CapsuleCollider hitboxCollider; // CAPSULE COLLIDER ARE THE HITBOXES

    // EnemyTakeDamage event
    public static event Action OnEnemyDeath;


    private bool animPresent = false;
    bool isDead;
    bool isSinking;

    private bool _canTakeDamage = false;

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) _canTakeDamage = true;
        else _canTakeDamage = false;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        
        _canTakeDamage = gameManager.State == GameState.InGame;
    }


    void SetInitReferences()
    {
        anim = GetComponent<Animator>();
        if (anim != null)
            animPresent = true;


        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        hitboxCollider = GetComponent<CapsuleCollider>();


        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        OnEnemyDeath?.Invoke();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        ScoreEvents.AddScore(scoreValue);

        hitboxCollider.isTrigger = true;

        if (animPresent)
            anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        StartSinking();
    }


    public void StartSinking()
    {
        GetComponent<EnemyMovement>().DisableMovement();
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}