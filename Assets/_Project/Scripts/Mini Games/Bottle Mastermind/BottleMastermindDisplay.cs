using TMPro;
using UnityEngine;

public class BottleMastermindDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text = null;

    public void OnGameWon()
    {
        _text.text = "Congrats, all bottles are in the right plates!";
    }

    public void OnGuessWrong(int correctDigits)
    {
        _text.text = $"You have {correctDigits} bottles in the right plates.";
    }
}
