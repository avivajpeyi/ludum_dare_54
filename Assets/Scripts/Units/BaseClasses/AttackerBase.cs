using System;
using UnityEngine;
using System.Collections;

public class AttackerBase : MonoBehaviour
{
    protected virtual bool _canAttack => GameManager.Instance.State == GameState.InGame;


    private void Awake()
    {
        SetInitReferences();
    }


    // SetInitReferences is an  method that is meant to be overridden by child classes
    public virtual void SetInitReferences()
    {
    }
}