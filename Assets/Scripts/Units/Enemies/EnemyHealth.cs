using UnityEngine;

[RequireComponent (typeof (ParticleSystem))]
[RequireComponent (typeof (AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    
    [SerializeField]
    [Tooltip("The CAPSULE hitbox collider of the enemy")]
    CapsuleCollider hitboxCollider; // CAPSULE COLLIDER ARE THE HITBOXES
    
    
    private bool animPresent = false;
    bool isDead;
    bool isSinking;


    void Awake()
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


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        hitboxCollider.isTrigger = true;

        if (animPresent)
            anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        
        StartSinking();
    }


    public void StartSinking()
    {
        // GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        // ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}