using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float baseDamage = 2f;

    float currentSpeed = 0;
    float currentDamage = 0;

    private void Awake() {
        currentSpeed = baseSpeed;
        currentDamage = baseDamage;
        PlayerPerkEvents.eventBuffDamage += BuffDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public float GetCurrentDamage()
    {
        return currentDamage;
    }
}
