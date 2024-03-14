using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MiniGame
{
    [SerializeField] private Button[] _simonButtons;
    [SerializeField] private TextMeshPro _textBox;

    public int SequenceLength;
    public float SequenceDelay = 1.0f;

    public event Action<ButtonType> IsButtonPressed;
    public event Action OnGameLost;
    public event Action OnGameStarted;
    public event Action SequenceWin;

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
            IsButtonPressed?.Invoke(buttonType);
            StartCoroutine(Waiting(0.5f));
            _endGame = ++_sequenceIndex >= _sequence.Length;
            NextSequence();
            StartCoroutine(PlayCurrentSequence(_sequenceIndex));
        }   
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
        yield return Waiting(SequenceDelay);
        _canPlay = false;
        for (int i = 0; i < currentIndex; i++)
        {
            Debug.Log("Button: " + _sequence[i]);
            _simonButtons[(int)_sequence[i]].HighlightButton(true);
            yield return Waiting(SequenceDelay);
            _simonButtons[(int)_sequence[i]].HighlightButton(false);
        }
        _canPlay = true;
    }

    private void NextSequence()
    {
        _currentSequenceIndex = 0;
        _textBox.text = $"{_sequenceIndex}/{SequenceLength}";
        if (_endGame)
        {
            OnGameWon?.Invoke();
        }
    }

    public void StartGame()
    {//TODO: Play a sound when the game starts //Feedback
        _textBox.text = "Start!";
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
        _currentSequence = Array.Empty<ButtonType>();

        foreach (var button in _simonButtons)
        {
            button.gameObject.SetActive(false);
        }
        OnGameLost?.Invoke();
    }

    public void ResetGame()
    {
        _textBox.text = "Regenerating sequence!";
        StartCoroutine(Waiting(0.5f));
        GenerateSequence();
    }

    private IEnumerator Waiting(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
