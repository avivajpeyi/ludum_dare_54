using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScaleUiEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 highlightedScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale when highlighted
    public float highlightDuration = 0.2f; // Duration of the scaling effect
    private Vector3 originalScale; // Store the original scale

    Button button;
    bool isButton = false;
    private void Start()
    {
        // Store the original scale of the button
        originalScale = transform.localScale;
        button = GetComponent<Button>();
        if (button != null)
            isButton = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        // if not interactable, don't scale
        if (isButton && !button.interactable)
            return;
        
        // Scale up when the pointer enters (hovered over) the button
        transform.DOScale(highlightedScale, highlightDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // if not interactable, don't scale
        if (isButton && !button.interactable)
            return;
        
        // Restore to the original scale when the pointer exits the button
        transform.DOScale(originalScale, highlightDuration);
    }
}
