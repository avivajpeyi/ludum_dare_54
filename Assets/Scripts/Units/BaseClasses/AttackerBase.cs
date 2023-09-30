using UnityEngine;
using System.Collections;

public class AttackerBase : MonoBehaviour
{
    protected bool _canAttack = false;

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) _canAttack = true;
        else _canAttack = false;
    }

    // SetInitReferences is an  method that is meant to be overridden by child classes
   public virtual void SetInitReferences() { }
}