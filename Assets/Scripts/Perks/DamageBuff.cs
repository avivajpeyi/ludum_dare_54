using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : PerkBase
{
    [SerializeField] private int damageToBuff = 1;

    public override void OnClick()
    {
        PlayerPerkEvents.BuffDamage(damageToBuff);
    }
}
