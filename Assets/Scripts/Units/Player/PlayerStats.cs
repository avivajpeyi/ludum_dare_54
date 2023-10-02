using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    [SerializeField] public float speed = 6f;
    [SerializeField] public float damage = 2f;

    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float maxDamage = 100f;
    public bool AtMaxSpeed => speed >= maxSpeed;
    public bool AtMaxDamage => damage >= maxDamage;
    
    // protected override void Awake()
    // {
    //     base.Awake();
    //     currentDamage = baseDamage;
    //     // PlayerPerkEvents.eventBuffDamage += BuffDamage;
    // }


    private void OnDestroy()
    {
        // PlayerPerkEvents.eventBuffDamage -= BuffDamage;
    }

    // public void BuffDamage(float damageToBuff)
    // {
    //     currentDamage += damageToBuff;
    // }
    //
    //
    // public float GetCurrentDamage()
    // {
    //     return currentDamage;
    // }
}