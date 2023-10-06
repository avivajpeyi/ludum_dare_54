using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PulseUiElement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Dotween scale Yoyo effect
        transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
