using UnityEngine;
using UnityEngine.Events;

public class BottleMastermindGame : MiniGame
{
    private const byte NUMBER_OF_BOTTLES = 5;
    private readonly int[] _secretCode = new int[NUMBER_OF_BOTTLES];
    private readonly int[] _guessCode = new int[NUMBER_OF_BOTTLES];

    [Tooltip("Event invoked when the player makes a wrong guess. The parameter is the number of correct digits in the guess.")]
    public UnityEvent<int> OnGuessWrong = new();

    private void Awake()
    {
        for (int i = 0; i < NUMBER_OF_BOTTLES; i++)
        {
            _secretCode[i] = i;
        }
        _secretCode.Shuffle();
        Debug.Log("Secret code: " + string.Join(", ", _secretCode));

        for (int i = 0; i < NUMBER_OF_BOTTLES; i++)
        {
            _guessCode[i] = -1;
        }
    }

    public void SetCodeDigit(int index, int value)
    {
        _guessCode[index] = value;
        CheckGuess();
    }

    private void CheckGuess()
    {
        int correctDigits = 0;
        for (int i = 0; i < NUMBER_OF_BOTTLES; i++)
        {
            if (_secretCode[i] == _guessCode[i])
            {
                correctDigits++;
            }
        }

        if (correctDigits == NUMBER_OF_BOTTLES)
        {
            OnGameWon.Invoke();
        }
        else
        {
            OnGuessWrong.Invoke(correctDigits);
        }
    }
}
