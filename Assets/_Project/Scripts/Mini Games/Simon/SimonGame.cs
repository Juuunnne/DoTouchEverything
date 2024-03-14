using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SimonGame : MiniGame
{
    [SerializeField] private SimonButton[] _simonButtons;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private TextMeshPro _textBox;

    [SerializeField] private int _sequenceLength = 5;
    [SerializeField] private float _sequenceDelay = 1.0f;

    [SerializeField] private UnityEvent OnSequenceFailed;

    private ButtonType[] _sequence;
    private int _sequenceIndex = 0;
    private int _currentSequenceIndex = 0;
    private bool _canPressButtons = false;

    private void OnEnable()
    {
        foreach (SimonButton button in _simonButtons)
        {
            button.OnButtonPressed += OnButtonPressed;
        }
    }

    private void OnDisable()
    {
        foreach (SimonButton button in _simonButtons)
        {
            button.OnButtonPressed -= OnButtonPressed;
        }
    }

    public void OnButtonPressed(ButtonType buttonType)
    {
        if (!_canPressButtons)
        {
            return;
        }

        if (_currentSequenceIndex < _sequenceIndex && buttonType != _sequence[_currentSequenceIndex])
        {
            OnSequenceFailed?.Invoke();
            _textBox.text = "You failed!";
            GameOver();
        }
        else
        {
            _currentSequenceIndex++;

            if (_currentSequenceIndex >= _sequenceIndex)
            {
                if (_sequenceIndex >= _sequence.Length)
                {
                    OnGameWon?.Invoke();
                    _textBox.text = "You won!";
                    GameOver();
                }
                else
                {
                    _sequenceIndex++;
                    _currentSequenceIndex = 0;
                    StartCoroutine(PlayCurrentSequence(_sequenceIndex));
                }
            }
        }

    }

    private void GenerateSequence()
    {
        _sequence = new ButtonType[_sequenceLength];
        Array values = Enum.GetValues(typeof(ButtonType));
        for (int i = 0; i < _sequenceLength; i++)
        {

            _sequence[i] = (ButtonType)values.GetValue(Random.Range(0, values.Length));
        }
        _sequenceIndex = 1;
        _currentSequenceIndex = 0;
    }

    private IEnumerator PlayCurrentSequence(int currentIndex)
    {
        _textBox.text = $"{_sequenceIndex}/{_sequenceLength}\nRemember the sequence!";
        _canPressButtons = false;
        for (int i = 0; i < currentIndex; i++)
        {
            yield return new WaitForSeconds(_sequenceDelay / 2);
            _simonButtons[(int)_sequence[i]].HighlightButton(true);
            yield return new WaitForSeconds(_sequenceDelay);
            _simonButtons[(int)_sequence[i]].HighlightButton(false);
        }
        _canPressButtons = true;
        _textBox.text = $"{_sequenceIndex}/{_sequenceLength}\nNow it's your turn!";
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(true);
        }
        _playButton.SetActive(false);
        _resetButton.SetActive(true);
        ResetGame();
    }

    [ContextMenu("Reset Game")]
    public void ResetGame()
    {
        _textBox.text = "Game Reset!";
        GenerateSequence();
        StartCoroutine(PlayCurrentSequence(_sequenceIndex));
    }

    [ContextMenu("End Game")]
    private void GameOver()
    {
        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(false);
        }
        _playButton.SetActive(true);
        _resetButton.SetActive(false);
    }

#if UNITY_EDITOR
    [ContextMenu("Press Top Button")] private void PressTopButton() => OnButtonPressed(ButtonType.ButtonUp);
    [ContextMenu("Press Left Button")] private void PressLeftButton() => OnButtonPressed(ButtonType.ButtonLeft);
    [ContextMenu("Press Center Button")] private void PressCenterButton() => OnButtonPressed(ButtonType.ButtonCenter);
    [ContextMenu("Press Right Button")] private void PressRightButton() => OnButtonPressed(ButtonType.ButtonRight);
    [ContextMenu("Press Bottom Button")] private void PressBottomButton() => OnButtonPressed(ButtonType.ButtonDown);
#endif
}
