using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField]
    private MiniGame[] _miniGames = new MiniGame[0];
    private int _miniGamesWon = 0;

    [SerializeField]
    private TextMeshPro _displayText;

    [SerializeField]
    private GameObject _exitDoor;

    private void Start()
    {
        foreach (var miniGame in _miniGames)
        {
            miniGame.OnGameWon.AddListener(OnGameWon);
        }
    }

    private void OnGameWon()
    {
        _miniGamesWon++;
        if (_miniGamesWon == _miniGames.Length)
        {
            _displayText.text = "Exit open!";
            _exitDoor.SetActive(false);
        }
        else
        {
            _displayText.text = _miniGamesWon + " / " + _miniGames.Length;
        }
    }
}
