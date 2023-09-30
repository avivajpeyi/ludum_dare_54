using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StaticInstance<PlayerStats>
{
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float baseDamage = 2f;

    float currentSpeed = 0;
    static float currentDamage = 0;

    private void Awake() {
        currentSpeed = baseSpeed;
        currentDamage = baseDamage;
        PlayerPerkEvents.eventBuffDamage += BuffDamage;
    }



    private void OnDestroy() {
        PlayerPerkEvents.eventBuffDamage -= BuffDamage;
    }

    void BuffDamage(float damageToBuff)
    {
        currentDamage += damageToBuff;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public static float GetCurrentDamage()
    {
        return currentDamage;
    }
}
