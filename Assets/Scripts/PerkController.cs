using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkController : MonoBehaviour
{
    
    [SerializeField]
    ParticleSystem FX;
    
    Button uziButton;
    Button rifleButton;
    Button sniperButton;
    Button pistolButton;
    Button damageButton;
    Button viewButton;
    Button speedButton;
    Button lightButton;
    Button[] myButtons;
    
    AudioClip selectionAudioClip;

    CameraManager cameraManager;


    private void Awake()
    {
        // PlayerPerkEvents.eventLevelUp += EnablePerkMenu;
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        // PlayerPerkEvents.eventLevelUp -= EnablePerkMenu;
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }


    void Start()
    {
        uziButton = transform.Find("UziButton").GetComponent<Button>();
        rifleButton = transform.Find("RifleButton").GetComponent<Button>();
        sniperButton = transform.Find("SniperButton").GetComponent<Button>();
        pistolButton = transform.Find("PistolButton").GetComponent<Button>();
        damageButton = transform.Find("DMGButton").GetComponent<Button>();
        viewButton = transform.Find("EYEButton").GetComponent<Button>();
        speedButton = transform.Find("SPDButton").GetComponent<Button>();
        lightButton = transform.Find("LITButton").GetComponent<Button>();

        // Populate myButtons array from children
        myButtons = GetComponentsInChildren<Button>(true);


        cameraManager = FindObjectOfType<CameraManager>();

        uziButton.onClick.AddListener(ChangeToUzi);
        rifleButton.onClick.AddListener(ChangeToRifle);
        sniperButton.onClick.AddListener(ChangeToSniper);
        pistolButton.onClick.AddListener(ChangeToPistol);
        damageButton.onClick.AddListener(IncreaseDMG);
        viewButton.onClick.AddListener(IncreaseView);
        speedButton.onClick.AddListener(IncreaseSpeed);
        lightButton.onClick.AddListener(IncreaseLight);

        selectionAudioClip = Resources.Load<AudioClip>("audio/upgrade");
        
        DisablePerkMenu(playFX:false);
    }


    void EnablePerkMenu()
    {
        // show menu
        foreach (var b in myButtons)
        {
            b.gameObject.SetActive(true);
        }

        
        
        if (PlayerStats.Instance.AtMaxSpeed)
            speedButton.interactable = false;

        if (cameraManager.AtMaxZoom())
            viewButton.interactable = false;

        if (PlayerStats.Instance.AtMaxDamage)
            damageButton.interactable = false;
        
        // TODO put buttons in a dict/list and loop through them
        if (PlayerWeaponsManager.Instance.activeWeapon == WeaponNames.Uzi)
            uziButton.interactable = false;
        else if (PlayerWeaponsManager.Instance.activeWeapon == WeaponNames.Rifle)
            rifleButton.interactable = false;
        else if (PlayerWeaponsManager.Instance.activeWeapon == WeaponNames.Sniper)
            sniperButton.interactable = false;
        else if (PlayerWeaponsManager.Instance.activeWeapon == WeaponNames.Pistol)
            pistolButton.interactable = false;

        Debug.Log("Perk Menu Enabled");
    }

    public void DisablePerkMenu(bool playFX=true)
    {
       
        // if myButtons is null, return
        if (myButtons == null)
            return;
        
        // hide menu
        foreach (var b in myButtons)
        {
            b.gameObject.SetActive(false);
        }


        if (playFX)
        {
            FX.Play();
            AudioSystem.Instance.PlaySound(selectionAudioClip, 0.3f);
            
        }
            
        
        Debug.Log("Perk Menu disabled");
    }


    void OnStateChanged(GameState state)
    {
        if (state == GameState.InUpgrades)
        {
            EnablePerkMenu();
            Debug.Log($"OnSetting {state}: Perk Menu Enabled");
        }
        else
        {
            DisablePerkMenu(false);
            Debug.Log($"OnSetting {state}: Perk Menu Disabled");
        }
    }


    // BUTTONS

    public void ChangeToUzi()
    {
        PlayerWeaponsManager.Instance.ChangeWeapon(WeaponNames.Uzi);
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void ChangeToRifle()
    {
        PlayerWeaponsManager.Instance.ChangeWeapon(WeaponNames.Rifle);
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void ChangeToSniper()
    {
        PlayerWeaponsManager.Instance.ChangeWeapon(WeaponNames.Sniper);
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void ChangeToPistol()
    {
        PlayerWeaponsManager.Instance.ChangeWeapon(WeaponNames.Pistol);
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void IncreaseDMG()
    {
        float damageToBuff = 1f;
        PlayerStats.Instance.damage += damageToBuff;
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void IncreaseSpeed()
    {
        PlayerStats.Instance.speed += 1;
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void IncreaseView()
    {
        float IncreaseViewAmount = 1f;
        PlayerPerkEvents.IncreaseView(IncreaseViewAmount);
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }

    public void IncreaseLight()
    {
        float IncreaseLightAmount = 2f;
        GameSpotlight.Instance.IncreaseSpotDistance();
        DisablePerkMenu();
        GameManager.Instance.ChangeState(GameState.InGame);
    }
}