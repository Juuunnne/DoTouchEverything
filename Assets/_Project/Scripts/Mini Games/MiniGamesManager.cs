using UnityEngine;
using UnityEngine.Events;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField]
    private MiniGame[] _miniGames = new MiniGame[0];
    private int _miniGamesWon = 0;

    public UnityEvent OnAllGamesWon = new();

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
            Debug.Log("All games won!");
            OnAllGamesWon.Invoke();
        }
    }
}
