using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevel : Singleton<PlayerLevel>
{
    public  int currentLevel = 1;
    public  int currentXP = 0;
    

    public  int XPneeded => Mathf.Max(10,currentLevel * 10);
    
    
    // On Increase XP Action
    public static Action OnIncreaseXP;
    
    

    void LevelUp()
    {
        currentLevel++;
        currentXP = 0;
        PlayerPerkEvents.LevelUp();
        GameManager.Instance.ChangeState(GameState.InUpgrades);
    }


    public void IncreaseXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= XPneeded)
        {
            LevelUp();
        }
        OnIncreaseXP?.Invoke();
    }
}