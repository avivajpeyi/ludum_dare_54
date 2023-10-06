using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInUpgradesPage : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    
    private void Awake()
    {
        // PlayerPerkEvents.eventLevelUp += EnablePerkMenu;
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        // PlayerPerkEvents.eventLevelUp -= EnablePerkMenu;
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }
    
    void OnStateChanged(GameState state)
    {
        if (state == GameState.InUpgrades)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
