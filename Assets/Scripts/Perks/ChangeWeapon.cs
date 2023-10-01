using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : PerkBase
{
    [SerializeField] private WeaponType weaponType = WeaponType.Uzi;

    public override void OnClick()
    {
        PlayerPerkEvents.ChangeWeapon(weaponType);
    }

    public void SetWeaponToChangeTo(WeaponType weaponType)
    {
        this.weaponType = weaponType;
    }
}
