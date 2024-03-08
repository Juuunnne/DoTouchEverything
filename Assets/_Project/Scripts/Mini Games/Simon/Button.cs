using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType : int
{
    ButtonUp,
    ButtonDown,
    ButtonLeft,
    ButtonRight,
    ButtonCenter
}

public class Button : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterial;
    
    [Header("Button Type")]
    public ButtonType buttonType;
    public Action<ButtonType> OnButtonPressed;
    
    public void HighlightButton(bool enable)
    {
        if (_highlightMaterial != null)
        {
            if (enable)
            {
                _highlightMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                _highlightMaterial.DisableKeyword("_EMISSION");
            }
        }
    }
   
    private IEnumerator HighlightOnPressed()
    {
        HighlightButton(true);
        yield return new WaitForSeconds(0.3f);
        HighlightButton(false);
    }

    public void OnButtonCollision()
    {
        Debug.Log("Button pressed: " + buttonType);
        OnButtonPressed?.Invoke(buttonType);
        StartCoroutine(HighlightOnPressed());
    }

}
