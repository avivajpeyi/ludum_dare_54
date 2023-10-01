using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    Dictionary<WeaponNames, GameObject> weapons =
        new Dictionary<WeaponNames, GameObject>();

    [SerializeField] bool debugMode = false;

    WeaponNames activeWeapon = WeaponNames.Pistol;

    List<WeaponNames> weaponsAvailible = new List<WeaponNames>();


    private void Awake()
    {
        foreach (var weapon in GetComponentsInChildren<WeaponBase>())
        {
            weapons.Add(weapon.GetComponent<WeaponBase>().name,
                weapon.gameObject);
            weaponsAvailible.Add(weapon.GetComponent<WeaponBase>().name);
        }

        ChangeWeapon(activeWeapon);
    }

    public void ChangeWeapon(WeaponNames weaponName)
    {
        activeWeapon = weaponName;
        foreach (var weapon in weapons)
        {
            weapon.Value.SetActive(false);
        }

        weapons[weaponName].SetActive(true);
    }


    private void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Random weapon from weaponsAvailible
                ChangeWeapon(weaponsAvailible[UnityEngine.Random.Range(0, weaponsAvailible.Count)]);
            }
        }
    }
}


[Serializable]
public enum WeaponNames
{
    Pistol,
    Uzi,
    Shotgun,
    Sniper,
    Rifle,
    RocketLauncher
}