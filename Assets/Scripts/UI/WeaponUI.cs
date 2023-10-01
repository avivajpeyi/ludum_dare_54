using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite uziSprite;

    private void Awake() {
        PlayerPerkEvents.eventChangeWeapon += SetWeaponDisplay;
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
        PlayerPerkEvents.eventChangeWeapon -= SetWeaponDisplay;
    }

    void SetWeaponDisplay(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Uzi:
                image.sprite = uziSprite;
                break;
            
            default:
                Debug.Log("SetWeaponDisplay switch fell through...");
                break;
        }
    }
}
