using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkTile : MonoBehaviour
{
    [SerializeField] private TMP_Text primaryText;
    [SerializeField] private TMP_Text secondaryText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrimaryText(string text)
    {
        primaryText.text = text;
    }

    public void SetSecondaryText(string text)
    {
        secondaryText.text = text;
    }

    public string GetPrimaryText()
    {
        return primaryText.text;
    }
}
