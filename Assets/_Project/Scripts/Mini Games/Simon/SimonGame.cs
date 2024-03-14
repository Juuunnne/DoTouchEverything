using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MiniGame
{
    [SerializeField] private Button[] _simonButtons;
    [SerializeField] private TextMeshPro _textBox;

    [SerializeField] private int _sequenceLength = 5;
    [SerializeField] private float _sequenceDelay = 1.0f;

    public event Action<ButtonType> IsButtonPressed;
    public event Action OnGameLost;
    public event Action OnGameStarted;
    public event Action SequenceWin;

    private ButtonType[] _sequence;
    private int _sequenceIndex = 0;
    private int _currentSequenceIndex = 0;
    private bool _canPressButtons = false;

    private void Start()
    {
        foreach (Button button in _simonButtons)
        {
            button.OnButtonPressed += OnButtonPressed;
        }
        _textBox.text = "Press to play!";
        OnGameWon.AddListener(GameOver);
    }

    private void OnDestroy()
    {
        foreach (Button button in _simonButtons)
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

        if (_currentSequenceIndex < _sequenceIndex)
        {
            if (buttonType == _sequence[_currentSequenceIndex])
            {
                _currentSequenceIndex++;
            }
            else
            {
                _textBox.text = "Wrong button!";
            }
        }

        // Has the player ended the sequence?
        if (_currentSequenceIndex >= _sequenceIndex)
        {
            _sequenceIndex++;
            if (_sequenceIndex >= _sequence.Length)
            {
                OnGameWon?.Invoke();
            }
            else
            {
                StartCoroutine(PlayCurrentSequence(_sequenceIndex));
                _currentSequenceIndex = 0;
            }
        }

        IsButtonPressed?.Invoke(buttonType);
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
        _textBox.text = $"Your current streak:\n{_sequenceIndex - 1}/{_sequenceLength}\nRemember the sequence!";
        _canPressButtons = false;
        for (int i = 0; i < currentIndex; i++)
        {
            yield return new WaitForSeconds(_sequenceDelay / 2);
            _simonButtons[(int)_sequence[i]].HighlightButton(true);
            yield return new WaitForSeconds(_sequenceDelay);
            _simonButtons[(int)_sequence[i]].HighlightButton(false);
        }
        _canPressButtons = true;
        _textBox.text = $"Your current streak:\n{_sequenceIndex - 1}/{_sequenceLength}\nNow it's your turn!";
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(true);
        }
        GenerateSequence();
        StartCoroutine(PlayCurrentSequence(_sequenceIndex));
        OnGameStarted?.Invoke();
    }

    private void GameOver()
    {
        _textBox.text = "Game Over!";
        _sequence = Array.Empty<ButtonType>();

        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(false);
        }
        OnGameLost?.Invoke();
    }

    [ContextMenu("Reset Game")]
    public void ResetGame()
    {
        _textBox.text = "Game Reset!";
        GenerateSequence();
    }

#if UNITY_EDITOR
    [ContextMenu("Press Top Button")] private void PressTopButton() => OnButtonPressed(ButtonType.ButtonUp);
    [ContextMenu("Press Left Button")] private void PressLeftButton() => OnButtonPressed(ButtonType.ButtonLeft);
    [ContextMenu("Press Center Button")] private void PressCenterButton() => OnButtonPressed(ButtonType.ButtonCenter);
    [ContextMenu("Press Right Button")] private void PressRightButton() => OnButtonPressed(ButtonType.ButtonRight);
    [ContextMenu("Press Bottom Button")] private void PressBottomButton() => OnButtonPressed(ButtonType.ButtonDown);
#endif
}
