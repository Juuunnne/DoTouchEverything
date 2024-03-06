using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MonoBehaviour
{
    public int SequenceLength = 10;
    public float SequenceDelay = 1.0f;
    [SerializeField] private Button[] _simonButtons;
    
    private ButtonType[] _sequence;
    private ButtonType[] _currentsequence;
    private int _currentsequenceIndex = 0;

    private void Start()
    {
        foreach (Button button in _simonButtons)
        {
            button.OnButtonPressed += OnButtonPressed;
        }
        GenerateSequence();
        StartCoroutine(PlaySequence());
    }

    private void OnDestroy()
    {
        foreach (Button button in _simonButtons)
        {
            button.OnButtonPressed -= OnButtonPressed;
        }
    }


    private void OnButtonPressed(ButtonType buttonType)
    {
        Debug.Log("Button pressed: " + buttonType);

        if (buttonType == _currentsequence[0])
        {
            ButtonType[] newSequence = new ButtonType[_sequence.Length - 1];
            for (int i = 0; i < newSequence.Length; i++)
            {
                newSequence[i] = _sequence[i + 1];
            }
            _currentsequence = newSequence;
            if (_sequence.Length == 0)
            {
                Debug.Log("Sequence completed");
                GenerateSequence();
                StartCoroutine(PlaySequence());
            }
        }
        else
        {
            Debug.Log("Wrong button pressed");
        }

        PlaySequence();
    }
    
    private void GenerateSequence()
    {
        _sequence = new ButtonType[SequenceLength];
        for (int i = 0; i < SequenceLength; i++)
        {
            _sequence[i] = (ButtonType)Random.Range(0, 5); //5 exclusive
        }
        _currentsequence = _sequence;
    }
    
    private IEnumerator PlaySequence()
    {
        _currentsequenceIndex++;
        for (int i = 0; i < _currentsequenceIndex; i++)
        {
            Debug.Log("Button: " + _sequence[_currentsequenceIndex]);
            _simonButtons[(int)_sequence[_currentsequenceIndex]].HighlightButton(true);
            yield return new WaitForSeconds(SequenceDelay);
            _simonButtons[(int)_sequence[_currentsequenceIndex]].HighlightButton(false);
            yield return new WaitForSeconds(SequenceDelay);
        }
    }
}
