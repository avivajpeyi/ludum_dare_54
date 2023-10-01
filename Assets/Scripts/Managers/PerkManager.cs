using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public enum PerkType
{
    DamageBuff,
    DamageBuffBuff,
    IncreaseView,
    Weapon
}

public enum WeaponType
{
    Uzi,
}

class ActivePerk
{
    public PerkType perkType;
    public int multiplyer = 1;
}

public class PerkManager : MonoBehaviour
{
    [SerializeField] private GameObject perkTilePrefab;
    [SerializeField] private GameObject activePerkUI;
    [SerializeField] private GameObject selectPerkUI;

    [SerializeField] private Sprite uziSprite;

    List<ActivePerk> currentPerks = new List<ActivePerk>();

    int numberOfPerksToPick = 2;
    
    private void Awake()
    {
        PlayerPerkEvents.eventLevelUp += ToggleUI;
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        PlayerPerkEvents.eventLevelUp -= ToggleUI;
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }
    

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            SetUiActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUiActive(false);
    }
    

    public void ToggleUI()
    {
        SetUiActive(!gameObject.activeSelf);
    }

    public void DisablePerksUi()
    {
        SetUiActive(false);
    }
    

    void SetUiActive(bool active)
    {
        gameObject.SetActive(active);
        if (gameObject.activeSelf)
        {
            PopulateSelectPerkList();
        }
        if (!active)
        {
            foreach (Transform child in selectPerkUI.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void PopulateSelectPerkList(){
        for (int i = 0; i < numberOfPerksToPick; i++)
        {
            int randomNumber = Mathf.CeilToInt(UnityEngine.Random.Range(0f, 4f));
            switch (randomNumber)
            {
                // case number ranges should be based on number of perks and their rarity
                case 1:
                    // Damage perk
                    InstantiatePerkTile(PerkType.DamageBuff);
                    break;

                case 2:
                    // Increase view perk
                    InstantiatePerkTile(PerkType.IncreaseView);
                    break;

                case 3:
                    // Increase damage more perk
                    InstantiatePerkTile(PerkType.DamageBuffBuff);
                    break;
                
                case 4:
                    // Uzi weapon perk
                    InstantiatePerkTile(PerkType.Weapon, WeaponType.Uzi);
                    break;
                
                default:
                    Debug.Log("PopulateSelectPerkList switch fell through...");
                    break;
            }
        }
    }

    void InstantiatePerkTile(PerkType perkType, WeaponType weaponType = WeaponType.Uzi)
    {
        GameObject perkTileObject = Instantiate(perkTilePrefab, selectPerkUI.transform);
        PerkTile perkTile = perkTileObject.GetComponent<PerkTile>();
        Button perkButton = perkTile.GetComponent<Button>();
        perkButton.onClick.AddListener(DisablePerksUi);
        perkButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.InGame));
        perkButton.onClick.AddListener(() => { AddSelectedPerkToActivePerksList(perkType, weaponType); });
        if (perkType != PerkType.Weapon)
        {
            switch (perkType)
            {
                case PerkType.DamageBuff:
                    DamageBuff damageBuff = perkTileObject.AddComponent<DamageBuff>();
                    perkTile.SetPrimaryText("DMG+");
                    perkTile.SetSecondaryText("");
                    perkButton.onClick.AddListener(damageBuff.OnClick);
                    
                    break;

                case PerkType.DamageBuffBuff:
                    DamageBuff damageBuffBuff = perkTileObject.AddComponent<DamageBuff>();
                    damageBuffBuff.SetDamageToBuff(2f);
                    perkTile.SetPrimaryText("DMG++");
                    perkTile.SetSecondaryText("");
                    perkButton.onClick.AddListener(damageBuffBuff.OnClick);
                    
                    break;

                case PerkType.IncreaseView:
                    IncreaseView increaseView = perkTileObject.AddComponent<IncreaseView>();
                    perkTile.SetPrimaryText("View+");
                    perkTile.SetSecondaryText("");
                    perkButton.onClick.AddListener(increaseView.OnClick);
                    
                    break;
                
                default:
                    Debug.Log("InstantiatePerkTile switch fell through...");
                    break;
            }

        }
        else
        {
            switch (weaponType)
            {
                case WeaponType.Uzi:
                    ChangeWeapon changeWeapon = perkTileObject.AddComponent<ChangeWeapon>();
                    perkTile.SetSprite(uziSprite);
                    perkButton.onClick.AddListener(changeWeapon.OnClick);
                    break;
                
                default:
                    Debug.Log("InstatiateWeaponTile switch fell through...");
                    break;
            }
        }

    }

    void AddSelectedPerkToActivePerksList(PerkType perkType, WeaponType weaponType)
    {
        var perk = currentPerks.Find(perk => perk.perkType == perkType);
        if (perk != null)
        {
            perk.multiplyer++;
            var perkTiles = activePerkUI.GetComponentsInChildren<PerkTile>();
            foreach (var perkTile in perkTiles)
            {
                if (perkType == PerkType.DamageBuff)
                {
                    Debug.Log("increment dmg+");
                    if (perkTile.GetPrimaryText() == "DMG+")
                    {
                        perkTile.SetSecondaryText("(" + perk.multiplyer + ")");
                    }
                }

                if (perkType == PerkType.DamageBuffBuff)
                {
                    if (perkTile.GetPrimaryText() == "DMG++")
                    {
                        perkTile.SetSecondaryText("(" + perk.multiplyer + ")");
                    }
                }

                if (perkType == PerkType.IncreaseView)
                {
                    if (perkTile.GetPrimaryText() == "View+")
                    {
                        perkTile.SetSecondaryText("(" + perk.multiplyer + ")");
                    }
                }
            }
        }
        else
        {
            var newPerk = new ActivePerk
            {
                perkType = perkType
            };
            currentPerks.Add(newPerk);
            GameObject perkTileObject = Instantiate(perkTilePrefab, activePerkUI.transform);
            PerkTile perkTile = perkTileObject.GetComponent<PerkTile>();
            perkTile.SetTextSmall();
            switch (perkType)
            {
                case PerkType.DamageBuff:
                    perkTile.SetPrimaryText("DMG+");
                    perkTile.SetSecondaryText("(" + newPerk.multiplyer + ")");
                    break;
                
                case PerkType.DamageBuffBuff:
                    perkTile.SetPrimaryText("DMG++");
                    perkTile.SetSecondaryText("(" + newPerk.multiplyer + ")");
                    break;
                
                case PerkType.IncreaseView:
                    perkTile.SetPrimaryText("View+");
                    perkTile.SetSecondaryText("(" + newPerk.multiplyer + ")");
                    break;
                
                default:
                    Debug.Log("AddSelectedPerkToActivePerkList switch fell through...");
                    break;
            }
        }
    }
}
