﻿using Cinemachine;
using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
public class Gun : AttackerBase
{
    public float damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;

    public float range = 100f;

    // Mathf.Clamp(accuracy, 0, 1);
    [Range(0, 1)] public float accuracy = 1f;

    private float missfireAmount => accuracy - 1;
    public int numBulletsInShot = 1;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    private float lightIntensity; 
    Sequence lightSequence;
    private CinemachineImpulseSource cameraImpulseSource;

    [SerializeField]
    private float shakeAmplitude = 0.5f;
    [SerializeField] private AudioClip sfx;


    // Override the base class's SetInitReferences() method


    public override void SetInitReferences()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        cameraImpulseSource = GetComponent<CinemachineImpulseSource>();
        lightIntensity = gunLight.intensity;
        CreateFlashTween();
        gunLight.intensity = 0;

        // Make sure gunLine has numBulletsInShot *2 positions
        gunLine.positionCount = numBulletsInShot * 2;
    }

    void CreateFlashTween()
    {
        lightSequence = DOTween.Sequence();
        lightSequence.Append(
            gunLight.DOIntensity(
                
                0 ,
                effectsDisplayTime* 2)
        );
        // lightSequence.OnComplete((() => Debug.Log("Light tween complete")));
        lightSequence.SetAutoKill(false);
        lightSequence.Pause();
    }


    protected override bool _canAttack
    {
        get
        {
            return (base._canAttack &&
                    Input.GetButton("Fire1") &&
                    timer >= timeBetweenBullets &&
                    Time.timeScale != 0
                );
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (_canAttack) Shoot();
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        // gunLight.enabled = false;
    }


    void _fireBulletIdx(int idx)
    {
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward + new Vector3(
            Random.Range(-missfireAmount, missfireAmount),
            Random.Range(-missfireAmount, missfireAmount), 0);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            Debug.Log($"<color=blue>Hit {shootHit.transform.name}</color>");
            EnemyHealth enemyHealth = shootHit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float dmg = damagePerShot + PlayerStats.Instance.damage;
                enemyHealth.TakeDamage(dmg, shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void Shoot()
    {
        timer = 0f;

        AudioSystem.Instance.PlaySound(sfx);
        // gunLight.enabled = true;
        FlashGunLight();
        
        CameraManager.Instance.Shake(cameraImpulseSource, shakeAmplitude);
        
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;

        _fireBulletIdx(0);
    }
    
    void FlashGunLight()
    {
        gunLight.intensity = lightIntensity;
        lightSequence.Restart();
        // lightSequence.Play();
    }
    
    
}