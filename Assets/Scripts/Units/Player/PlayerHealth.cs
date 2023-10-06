using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cinemachine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : HealthBase
{
    
    CinemachineImpulseSource impulseSource;
    public Slider healthSlider;
    bool healthSliderPresent = false;
    public Image damageImage;
    private bool damageImagePresent = false;
    
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    

    bool damaged;
    
    
    protected override void SetInitReferences()
    {
        
        if (damageImage != null)
        {
            damageImagePresent = true;
        }
        
        if (healthSlider != null)
        {
            healthSliderPresent = true;
            healthSlider.maxValue = startingHealth;
            healthSlider.value = startingHealth;
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    void Update()
    {
        if (!_canTakeDamage) return;
        FlashDamageImage();
        damaged = false;
    }


    void FlashDamageImage()
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
    }


    public void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        CameraManager.Instance.Shake(impulseSource, 1f);
        if (healthSliderPresent)
            healthSlider.value = currentHealth;
        
    }
    


    protected override void Death()
    {
        base.Death();
        GameManager.Instance.ChangeState(GameState.GameOver);
    }
}