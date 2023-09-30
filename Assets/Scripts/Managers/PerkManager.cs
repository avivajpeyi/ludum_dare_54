using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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
            int randomNumber = Mathf.CeilToInt(UnityEngine.Random.Range(0f, 2f));
            switch (randomNumber)
            {
                // case number ranges should be based on number of perks and their rarity
                case 1:
                    // Damage perk
                    InstantiatePerkTile("DMG+", "", selectPerkUI.transform);
                    break;

                case 2:
                    // Increase view perk
                    InstantiatePerkTile("View+", "", selectPerkUI.transform);
                    break;
                
                default:
                    Debug.Log("PopulateSelectPerkList switch fell through...");
                    break;
            }
        }
    }

    void InstantiatePerkTile(string primaryText, string secondaryText, Transform parent)
    {
        GameObject perkTileObject = Instantiate(perkTilePrefab, parent);
        PerkTile perkTile = perkTileObject.GetComponent<PerkTile>();
        perkTile.SetPrimaryText(primaryText);
        perkTile.SetSecondaryText(secondaryText);

    }
}
