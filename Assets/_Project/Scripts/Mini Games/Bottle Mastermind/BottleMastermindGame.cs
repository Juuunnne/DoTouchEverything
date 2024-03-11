using UnityEngine;
using UnityEngine.Events;

public class BottleMastermindGame : MiniGame
{
    [Tooltip("Event invoked when the player makes a wrong guess. The parameter is the number of correct digits in the guess.")]
    public UnityEvent<int> OnGuessWrong = new();

    [SerializeField]
    private BottleSocket[] _sockets = new BottleSocket[0];

    private void Start()
    {
        int[] code = new int[_sockets.Length];
        for (int i = 0; i < _sockets.Length; i++)
        {
            code[i] = i;
        }
        code.Shuffle();

        for (int i = 0; i < _sockets.Length; i++)
        {
            _sockets[i].WantedBottleDigit = code[i];
        }
    }

    public void CheckGuess()
    {
        int correctDigits = 0;
        for (int i = 0; i < _sockets.Length; i++)
        {
            if (_sockets[i].IsDigitCorrect)
            {
                correctDigits++;
            }
        }

        if (correctDigits == _sockets.Length)
        {
            OnGameWon.Invoke();
        }
        else
        {
            OnGuessWrong.Invoke(correctDigits);
        }
    }
}
