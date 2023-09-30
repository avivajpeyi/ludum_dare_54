using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPerkEventsSO", menuName = "SOs/PlayerPerkEvents")]
public class PlayerPerkEvents : ScriptableObject
{

    public delegate void BuffDamageDelegate(float buffAmount);
    public static event BuffDamageDelegate eventBuffDamage;
    public static void BuffDamage(float buffAmount)
    {
        if (eventBuffDamage != null)
        {
            eventBuffDamage(buffAmount);
        }
    }

    public delegate void IncreaseViewDelegate(float IncreaseViewAmount);
    public static event IncreaseViewDelegate eventIncreaseView;
    public static void IncreaseView(float IncreaseViewAmount)
    {
        if (eventIncreaseView != null)
        {
            eventIncreaseView(IncreaseViewAmount);
        }
    }

    public delegate void LevelUpDelegate();
    public static event LevelUpDelegate eventLevelUp;
    public static void LevelUp()
    {
        // THIS SHOULD BE HANDLED BY THE PLAYER LEVEL UP SCRIPT
        if (eventLevelUp != null)
        {
            eventLevelUp();
        }
    }
}
