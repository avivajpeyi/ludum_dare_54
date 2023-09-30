using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected bool _canMove;

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
    }

    protected virtual void Start()
    {
        if (GameManager.Instance.State == GameState.InGame) _canMove = true;
    }

    protected void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    protected virtual void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) _canMove = true;
        else _canMove = false;
    }


    protected virtual void SetInitReferences()
    {
    }
}