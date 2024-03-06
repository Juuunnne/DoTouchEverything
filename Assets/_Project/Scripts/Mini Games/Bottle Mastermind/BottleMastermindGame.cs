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
        for (int i = 0; i < NUMBER_OF_BOTTLES; i++)
        {
            _secretCode[i] = i;
            _guessCode[i] = -1;
            int index = i;
            _bottleInteractors[i].selectEntered.AddListener(args => { OnObjectEnterSocket(index, args.interactableObject); });
            _bottleInteractors[i].selectExited.AddListener(args => { OnObjectLeaveSocket(index); });
        }
        Debug.Log("Secret code: " + string.Join(", ", _secretCode));
    }

    private void OnObjectEnterSocket(int index, IXRSelectInteractable interactableObject)
    {
        if (interactableObject.transform.TryGetComponent(out Bottle bottle))
        {
            _guessCode[index] = bottle.Digit;
        }
        else
        {
            _guessCode[index] = -1;
        }

        CheckGuess();
    }

    private void OnObjectLeaveSocket(int index)
    {
        _guessCode[index] = -1;
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

    private void OnValidate()
    {
        if (_bottleInteractors.Length != NUMBER_OF_BOTTLES)
        {
            Debug.LogError("The number of bottle interactors must be equal to " + NUMBER_OF_BOTTLES);
        }
    }
}
