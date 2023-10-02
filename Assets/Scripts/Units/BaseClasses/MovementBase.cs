using UnityEngine;

public class MovementBase : MonoBehaviour
{
    private HealthBase myHealth;
    protected Animator anim;
    protected bool animPresent;

    protected bool _canMove
    {
        get
        {
            return (!myHealth.isDead &&
                    GameManager.Instance.State == GameState.InGame
                );
        }
    }


    protected virtual void Awake()
    {
        myHealth = GetComponent<HealthBase>();
        anim = GetComponent <Animator> ();
        if (anim != null) {
            animPresent = true;
        }
        SetInitReferences();
    }

    protected virtual void SetInitReferences()
    {
    }
}