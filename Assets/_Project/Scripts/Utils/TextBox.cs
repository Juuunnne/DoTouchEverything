using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    void Update()
    {
        if (_background)
        {
            if (_text.text == "")
            {
                _background.enabled = false;
            }
            else
            {
                _background.enabled = true;
                _background.rectTransform.sizeDelta = new Vector2(_text.preferredWidth + 20, _text.preferredHeight + 20);
            }
        }
    }
    
    public void SetText(string text)
    {
        _text.text = text;
    }
}
