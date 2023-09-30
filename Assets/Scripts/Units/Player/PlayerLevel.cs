using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevel : StaticInstance<PlayerLevel>
{
    public static int currentLevel = 1;
    public static int currentXP = 0;
    public static int XPneeded => Mathf.Max(100,currentLevel * 10);


    static void LevelUp()
    {
        currentLevel++;
        currentXP = 0;
    }


    public static void IncreaseXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= XPneeded)
        {
            LevelUp();
        }
    }
}