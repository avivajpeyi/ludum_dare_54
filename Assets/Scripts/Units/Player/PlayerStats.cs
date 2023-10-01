using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    [SerializeField] public float speed = 6f;
    [SerializeField] private float baseDamage = 2f;

    float currentSpeed = 0;
    static float currentDamage = 0;

    protected override void Awake()
    {
        base.Awake();
        currentDamage = baseDamage;
        PlayerPerkEvents.eventBuffDamage += BuffDamage;
    }


    private void OnDestroy()
    {
        PlayerPerkEvents.eventBuffDamage -= BuffDamage;
    }

    public void BuffDamage(float damageToBuff)
    {
        currentDamage += damageToBuff;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public float GetCurrentDamage()
    {
        return currentDamage;
    }
}