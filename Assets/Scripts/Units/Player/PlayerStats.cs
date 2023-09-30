using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private int baseDamage = 2;

    float currentSpeed = 1f;
    int currentDamage = 2;

    private void Awake() {
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

    void BuffDamage(int damageToBuff)
    {
        currentDamage += damageToBuff;
    }
}
