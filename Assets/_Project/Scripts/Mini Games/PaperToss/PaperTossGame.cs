using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PaperTossGame : MiniGame
{
    public UnityEvent<int> OnUpdateScore = new ();

    [SerializeField]
    private int _maxScore = 3;

    [SerializeField]
    private TextMeshPro _text = null;

    private int _currentScore = 0;

    private void Start()
    {
        _currentScore = 0;
    }

    public void IncrementScore()
    {
        OnUpdateScore.Invoke(++_currentScore);

        if (_currentScore >= _maxScore)
        {
            OnGameWon.Invoke();
            _text.text = "You won!";
        }
    }
}
