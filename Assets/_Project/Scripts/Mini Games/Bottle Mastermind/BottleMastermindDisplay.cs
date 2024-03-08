using TMPro;
using UnityEngine;

public class BottleMastermindDisplay : MonoBehaviour
{
    [SerializeField]
    private BottleMastermindGame _game = null;

    [SerializeField]
    private TextMeshPro _text = null;

    private void Start()
    {
        _game.OnGameWon.AddListener(OnGameWon);
        _game.OnGuessWrong.AddListener(OnGuessWrong);
    }

    private void OnGameWon()
    {
        _text.text = "You won!";
    }

    private void OnGuessWrong(int correctDigits)
    {
        _text.text = "Score:\n" + correctDigits;
    }

    private void OnDestroy()
    {
        _game.OnGameWon.RemoveListener(OnGameWon);
        _game.OnGuessWrong.RemoveListener(OnGuessWrong);
    }
}
