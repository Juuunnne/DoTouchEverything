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

public class SimonButton : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private AudioClip _buttonSound;
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
        OnButtonPressed?.Invoke(buttonType);
        StartCoroutine(HighlightOnPressed());
        if (_buttonSound != null)
        {
            AudioSource.PlayClipAtPoint(_buttonSound, transform.position);
        }
    }

}
