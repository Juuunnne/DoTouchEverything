using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MiniGame
{
    [SerializeField] private Button[] _simonButtons;
    [SerializeField] private TextBox _textBox;
    
    public int SequenceIndex
    {
        get => _sequenceIndex;
        set => _sequenceIndex = value > _sequence.Length ? _sequence.Length : value;
    }
    public int SequenceLength = 10;
    public float SequenceDelay = 1.0f;
    
    private ButtonType[] _sequence;
    private ButtonType[] _currentSequence;
    private int _sequenceIndex = 0;
    private int _currentSequenceIndex = 0;
    private bool _canPlay = false;

    private void Start()
    {
        foreach (Button button in _simonButtons)
        {
            button.OnButtonPressed += OnButtonPressed;
        }
        GenerateSequence();
        StartCoroutine(PlayCurrentSequence(_sequenceIndex));
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
        if (!_canPlay)
        {
            return;
        }
        
        if (_currentSequenceIndex < _sequenceIndex)
        {
            if (buttonType == _currentSequence[_currentSequenceIndex])
            {
                _currentSequenceIndex++;
            }
            else
            {
                Debug.Log("Wrong button pressed");
            }
        }
        if (_currentSequenceIndex >=  _sequenceIndex)
        {
            _textBox.SetText("Wait for the next sequence!");
            NextSequence();
            StartCoroutine(PlayCurrentSequence(++_sequenceIndex));
        }
        
        if (_sequence.Length == 0)
        {
            Debug.Log("Sequence completed");
            OnGameWon.Invoke();
        }
    }
    
    private void GenerateSequence()
    {
        _sequence = new ButtonType[SequenceLength];
        for (int i = 0; i < SequenceLength; i++)
        {
            _sequence[i] = (ButtonType)Random.Range(0, 5); //5 exclusive
        }
        _sequenceIndex = 1;
        _currentSequenceIndex = 0;
        _currentSequence = _sequence;
    }
    
    private IEnumerator PlayCurrentSequence(int currentIndex)
    { 
        yield return new WaitForSeconds(1);
        _canPlay = false;
        for (int i = 0; i < currentIndex; i++)
        {
            Debug.Log("Button: " + _currentSequence[i]);
            _simonButtons[(int)_currentSequence[i]].HighlightButton(true);
            yield return new WaitForSeconds(SequenceDelay);
            _simonButtons[(int)_currentSequence[i]].HighlightButton(false);
        }
        _canPlay = true;
    }
    
    private void NextSequence()
    {
        _currentSequenceIndex = 0;
        ButtonType[] newSequence = new ButtonType[_currentSequence.Length - 1];
        for (int i = 0; i < newSequence.Length; i++)
        {
            newSequence[i] = _currentSequence[i + 1];
        }
        _currentSequence = newSequence;
    }
}
