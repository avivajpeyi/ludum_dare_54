using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPerkEventsSO", menuName = "SOs/PlayerPerkEvents")]
public class PlayerPerkEvents : ScriptableObject
{

    public delegate void BuffDamageDelegate(int buffAmount);
    public static event BuffDamageDelegate eventBuffDamage;
    public static void BuffDamage(int buffAmount)
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
}
