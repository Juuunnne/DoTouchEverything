using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameWon = new();
}
