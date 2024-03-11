using TMPro;
using UnityEngine;

public class BottleMastermindDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text = null;

    public void OnGameWon()
    {
        _text.text = "You won!";
    }

    public void OnGuessWrong(int correctDigits)
    {
        _text.text = "Score:\n" + correctDigits;
    }
}
