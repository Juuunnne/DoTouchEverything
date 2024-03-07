using UnityEngine;
using UnityEngine.Events;

public class PaperTossGame : MiniGame
{
    [Tooltip("Event invoked when the player makes a wrong throw.")]
    public UnityEvent OnThrowWrong = new UnityEvent();

    [SerializeField]
    private float _maxScore = 10;

    private float _currentScore = 0;

    private void Start()
    {
        _currentScore = 0;
    }

    public void IncrementScore()
    {
        _currentScore++;
        Debug.Log("Score: " + _currentScore);

        if (_currentScore >= _maxScore)
        {
            Debug.Log("Game Won");
            OnGameWon.Invoke();
        }
    }

    public void NotifyThrowWrong()
    {
        OnThrowWrong.Invoke();
    }
}
