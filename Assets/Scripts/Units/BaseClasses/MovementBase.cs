using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected bool _canMove;
    protected GameManager gm;
    // protected Animator anim; 

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        SetInitReferences();
        gm = FindObjectOfType<GameManager>();
        if (gm.State == GameState.InGame) _canMove = true;
        // anim = GetComponent<Animator>();

    }

    protected  void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    protected virtual void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) _canMove = true;
        else _canMove = false;
    }


    protected virtual void SetInitReferences()
    {
       
    }
}