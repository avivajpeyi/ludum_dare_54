using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
public class PlayerShooting : AttackerBase
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

    [SerializeField] private AudioClip sfx;


    // Override the base class's SetInitReferences() method


    public override void SetInitReferences()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

        // Make sure gunLine has numBulletsInShot *2 positions
        gunLine.positionCount = numBulletsInShot * 2;
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
        gunLight.enabled = false;
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
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float dmg = damagePerShot + PlayerStats.Instance.GetCurrentDamage();
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
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;

        _fireBulletIdx(0);
    }
}