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
    IncreaseView
}

public class PerkManager : MonoBehaviour
{
    [SerializeField] private GameObject perkTilePrefab;
    [SerializeField] private GameObject activePerkUI;
    [SerializeField] private GameObject selectPerkUI;

    List<PerkBase> currentPerks;

    int numberOfPerksToPick = 2;
    
    private void Awake()
    {
        PlayerPerkEvents.eventLevelUp += ToggleUI;
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
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
    
    

    private void SetInitReferences() {
        
        
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
        if (active)
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
            int randomNumber = Mathf.CeilToInt(UnityEngine.Random.Range(0f, 3f));
            switch (randomNumber)
            {
                // case number ranges should be based on number of perks and their rarity
                case 1:
                    // Damage perk
                    InstantiatePerkTile("DMG+", "", selectPerkUI.transform, PerkType.DamageBuff);
                    break;

                case 2:
                    // Increase view perk
                    InstantiatePerkTile("View+", "", selectPerkUI.transform, PerkType.IncreaseView);
                    break;

                case 3:
                    // Increase view perk
                    InstantiatePerkTile("DMG++", "", selectPerkUI.transform, PerkType.DamageBuffBuff);
                    break;
                
                default:
                    Debug.Log("PopulateSelectPerkList switch fell through...");
                    break;
            }
        }
    }

    void InstantiatePerkTile(string primaryText, string secondaryText, Transform parent, PerkType perkType)
    {
        GameObject perkTileObject = Instantiate(perkTilePrefab, parent);
        PerkTile perkTile = perkTileObject.GetComponent<PerkTile>();
        perkTile.SetPrimaryText(primaryText);
        perkTile.SetSecondaryText(secondaryText);
        Button perkButton = perkTile.GetComponent<Button>();
        perkButton.onClick.AddListener(DisablePerksUi);
        perkButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.InGame));
        switch (perkType)
        {
            case PerkType.DamageBuff:
                DamageBuff damageBuff = perkTileObject.AddComponent<DamageBuff>();
                perkButton.onClick.AddListener(damageBuff.OnClick);
                
                break;

            case PerkType.DamageBuffBuff:
                DamageBuff damageBuffBuff = perkTileObject.AddComponent<DamageBuff>();
                damageBuffBuff.SetDamageToBuff(2f);
                perkButton.onClick.AddListener(damageBuffBuff.OnClick);
                
                break;

            case PerkType.IncreaseView:
                IncreaseView increaseView = perkTileObject.AddComponent<IncreaseView>();
                perkButton.onClick.AddListener(increaseView.OnClick);
                
                break;
            
            default:
                Debug.Log("InstantiatePerkTile switch fell through...");
                break;
        }
    }
}
