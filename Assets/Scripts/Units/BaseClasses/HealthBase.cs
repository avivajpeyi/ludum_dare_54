using UnityEngine;

public class HealthBase : MonoBehaviour
{
    protected float startingHealth = 100f;
    public float currentHealth { get; protected set; }
    protected Animator anim;
    protected bool animPresent;

    [SerializeField]
    GameObject hitParticles;
    [SerializeField]
    AudioClip hitClip;
    [SerializeField]
    AudioClip deathClip;


    public bool isDead
    {
        get { return (currentHealth <= 0); }
    }

    protected bool _canTakeDamage
    {
        get
        {
            return GameManager.Instance.State == GameState.InGame &&
                   !isDead;
        }
    }

    protected virtual void Awake()
    {
        if (hitParticles == null)
        {
            hitParticles = Resources.Load<GameObject>("FXs/BloodFX");
        }
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        animPresent = (anim != null);
        
        SetInitReferences();
    }


    protected virtual void SetInitReferences()
    {
    }
    
    public virtual void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (!_canTakeDamage) return;
        AudioSystem.Instance.PlaySound(hitClip);
        currentHealth -= amount;
        Instantiate(hitParticles, hitPoint, Quaternion.identity);
        if (isDead) Death();
    }

    public virtual void TakeDamage(float amount)
    {
        TakeDamage(amount, gameObject.transform.position);
    }


    protected virtual void Death()
    {
        if (animPresent)
            anim.SetTrigger("Dead");
        AudioSystem.Instance.PlaySound(deathClip);
    }
    
    
}