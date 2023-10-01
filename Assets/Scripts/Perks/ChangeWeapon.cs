using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : PerkBase
{
    [SerializeField] private WeaponNames weaponType = WeaponNames.Uzi;

    public override void OnClick()
    {
        PlayerPerkEvents.ChangeWeapon(weaponType);
    }

    public void SetWeaponToChangeTo(WeaponNames weaponType)
    {
        this.weaponType = weaponType;
    }
}
