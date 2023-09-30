using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseView : PerkBase
{
    [SerializeField] private float viewIncreaseIncrement = 0.2f;

    public override void OnClick()
    {
        PlayerPerkEvents.IncreaseView(viewIncreaseIncrement);
    }
}
