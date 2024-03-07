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
    private ButtonType[] _currentSequence;
    private int _currentSequenceIndex = 0;

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

        if (buttonType == _currentSequence[0])
        {
            ButtonType[] newSequence = new ButtonType[_sequence.Length - 1];
            for (int i = 0; i < newSequence.Length; i++)
            {
                newSequence[i] = _sequence[i + 1];
            }
            _currentSequence = newSequence;
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

        StartCoroutine(PlaySequence());
    }
    
    private void GenerateSequence()
    {
        _sequence = new ButtonType[SequenceLength];
        for (int i = 0; i < SequenceLength; i++)
        {
            _sequence[i] = (ButtonType)Random.Range(0, 5); //5 exclusive
        }
        _currentSequence = _sequence;
    }
    
    private IEnumerator PlaySequence()
    {
        _currentSequenceIndex++;
        for (int i = 0; i < _currentSequenceIndex; i++)
        {
            Debug.Log("Button: " + _sequence[_currentSequenceIndex]);
            _simonButtons[(int)_sequence[_currentSequenceIndex]].HighlightButton(true);
            yield return new WaitForSeconds(SequenceDelay);
            _simonButtons[(int)_sequence[_currentSequenceIndex]].HighlightButton(false);
        }
    }
}
