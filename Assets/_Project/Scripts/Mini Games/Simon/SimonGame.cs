using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MiniGame
{
    [SerializeField] private Button[] _simonButtons;

    public event Action<ButtonType> IsButtonPressed;

    [SerializeField] private TextMeshPro _textBox;

    public int SequenceIndex
    {
        get => _sequenceIndex;
        set => _sequenceIndex = value > _sequence.Length ? _sequence.Length - 1 : value;
    }
    public int SequenceLength = 10;
    public float SequenceDelay = 1.0f;

    private ButtonType[] _sequence;
    private ButtonType[] _currentSequence;
    private int _sequenceIndex = 0;
    private int _currentSequenceIndex = 0;
    private bool _canPlay = false;
    private bool _endGame = false;


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
        Debug.Log("Button pressed: " + buttonType);
        if (!_canPlay)
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
        else if (_currentSequenceIndex >=  _sequenceIndex)
        {
            _textBox.text = "Wait for the next sequence!";
            _endGame = ++_sequenceIndex >= _sequence.Length;
            NextSequence();
            StartCoroutine(PlayCurrentSequence(_sequenceIndex));
        }

        if (_sequence.Length == 0)
        {
            Debug.Log("Sequence completed");
            OnGameWon.Invoke();
        }

        IsButtonPressed?.Invoke(buttonType);
    }

    [ContextMenu("Generate Sequence")]
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
            Debug.Log("Button: " + _sequence[i]);
            _simonButtons[(int)_sequence[i]].HighlightButton(true);
            yield return new WaitForSeconds(SequenceDelay);
            _simonButtons[(int)_sequence[i]].HighlightButton(false);
        }
        _canPlay = true;
    }

    private void NextSequence()
    {
        if (_sequence.Length == 0)
        {
            Debug.Log("Sequence completed");
            OnGameWon.Invoke();
        }
        _textBox.text = $"{_sequenceIndex}/{SequenceLength}";
    }

    private void GameOver()
    {
        _textBox.text = "Game Over!";
        _sequence = Array.Empty<ButtonType>();
        _currentSequence = Array.Empty<ButtonType>();

        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        _textBox.text = "Start!";
        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(true);
        }
        GenerateSequence();
        StartCoroutine(PlayCurrentSequence(_sequenceIndex));
    }
}
