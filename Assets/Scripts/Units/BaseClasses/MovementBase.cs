using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected bool _canMove = false;

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) _canMove = true;
        else _canMove = false;
    }


    protected virtual void SetInitReferences()
    {
    }
}