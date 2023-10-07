using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInSpecificState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameState stateToDisplayIn = GameState.InUpgrades;
    
    private void Awake()
    {
        // PlayerPerkEvents.eventLevelUp += EnablePerkMenu;
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    private void Start()
    {
        SetActiveIfCorrectState(GameManager.Instance.State);
    }
    

    private void OnDestroy()
    {
        // PlayerPerkEvents.eventLevelUp -= EnablePerkMenu;
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }
    
    void OnStateChanged(GameState state)
    {
        SetActiveIfCorrectState(state);
    }

    void SetActiveIfCorrectState(GameState state)
    {
        if (state == stateToDisplayIn)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
