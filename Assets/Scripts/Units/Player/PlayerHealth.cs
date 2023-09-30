using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public bool healthSliderPresent = false;
    public Image damageImage;
    private bool damageImagePresent = false;
    private bool animPresent = false;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        if (damageImage != null)
        {
            damageImagePresent = true;
        }
        if (anim != null)
        {
            animPresent = true;
        }
        if (healthSlider != null)
        {
            healthSliderPresent = true;
            healthSlider.maxValue = startingHealth;
            healthSlider.value = startingHealth;
        }
    }


    void Update()
    {
        if (damaged)
        {
            if (damageImagePresent)
                damageImage.color = flashColour;
        }
        else
        {
            if (damageImagePresent)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear,
                    flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        if (healthSliderPresent)
            healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        if (animPresent)
            anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}