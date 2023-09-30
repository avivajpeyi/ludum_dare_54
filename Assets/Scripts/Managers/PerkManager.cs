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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            PopulateSelectPerkList();
        }
        if (!gameObject.activeSelf)
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
        switch (perkType)
        {
            case PerkType.DamageBuff:
                DamageBuff damageBuff = perkTileObject.AddComponent<DamageBuff>();
                perkTile.GetComponent<Button>().onClick.AddListener(damageBuff.OnClick);
                perkTile.GetComponent<Button>().onClick.AddListener(ToggleUI);
                break;

            case PerkType.DamageBuffBuff:
                DamageBuff damageBuffBuff = perkTileObject.AddComponent<DamageBuff>();
                damageBuffBuff.SetDamageToBuff(2f);
                perkTile.GetComponent<Button>().onClick.AddListener(damageBuffBuff.OnClick);
                perkTile.GetComponent<Button>().onClick.AddListener(ToggleUI);
                break;

            case PerkType.IncreaseView:
                IncreaseView increaseView = perkTileObject.AddComponent<IncreaseView>();
                perkTile.GetComponent<Button>().onClick.AddListener(increaseView.OnClick);
                perkTile.GetComponent<Button>().onClick.AddListener(ToggleUI);
                break;
            
            default:
                Debug.Log("InstantiatePerkTile switch fell through...");
                break;
        }
    }
}
