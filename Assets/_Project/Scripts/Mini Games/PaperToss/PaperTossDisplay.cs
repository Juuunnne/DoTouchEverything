using TMPro;
using UnityEngine;

public class PaperTossDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text = null;

    public void OnGameWon()
    {
        _text.text = "You won!";
    }

    public void OnUpdateScore(int score)
    {
        _text.text = $"Score: {score}";
    }
}
