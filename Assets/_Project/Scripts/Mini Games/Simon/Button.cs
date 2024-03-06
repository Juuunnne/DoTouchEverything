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

    public Action<ButtonType> OnButtonPressed;
    
    [Header("Button Type")]
    public ButtonType buttonType;
    
    [SerializeField] private Material _highlightMaterial;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            OnButtonPressed?.Invoke(buttonType);
            StartCoroutine(HighlightOnPressed());
        }
    }

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

}
