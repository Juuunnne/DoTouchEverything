using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BottleMastermindGame : MiniGame
{
    private const byte NUMBER_OF_BOTTLES = 4;
    private readonly int[] _secretCode = new int[NUMBER_OF_BOTTLES];
    private readonly int[] _guessCode = new int[NUMBER_OF_BOTTLES];

    [Tooltip("Event invoked when the player makes a wrong guess. The parameter is the number of correct digits in the guess.")]
    public UnityEvent<int> OnGuessWrong = new();

    [SerializeField]
    private XRSocketInteractor[] _bottleInteractors = new XRSocketInteractor[0];

    private void Awake()
    {
        for (int i = 0; i < NUMBER_OF_BOTTLES - 1; i++)
        {
            _secretCode[i] = i;
        }
        _secretCode.Shuffle();
        Debug.Log("Secret code: " + string.Join(", ", _secretCode));

        for (int i = 0; i < NUMBER_OF_BOTTLES - 1; i++)
        {
            _guessCode[i] = -1;
        }

        for (int i = 0; i < NUMBER_OF_BOTTLES - 1; i++)
        {
            _bottleInteractors[i].selectEntered.AddListener(args => SetCodeDigit(i, args));
            _bottleInteractors[i].selectExited.AddListener(args => SetCodeDigit(i, null));
        }
    }

    private void SetCodeDigit(int index, SelectEnterEventArgs args)
    {
        if (args?.interactableObject.transform.TryGetComponent(out Bottle bottle) is true)
        {
            _guessCode[index] = bottle.Digit;
        }
        else
        {
            _guessCode[index] = -1;
        }

        CheckGuess();
    }

    private void CheckGuess()
    {
        int correctDigits = 0;
        for (int i = 0; i < NUMBER_OF_BOTTLES - 1; i++)
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

    private void OnValidate()
    {
        if (_bottleInteractors.Length != NUMBER_OF_BOTTLES)
        {
            Debug.LogError("The number of bottle interactors must be equal to " + NUMBER_OF_BOTTLES);
        }
    }
}
