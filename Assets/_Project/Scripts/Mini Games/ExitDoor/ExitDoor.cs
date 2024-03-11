using TMPro;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject _exitDoor;

    [SerializeField]
    private TextMeshPro _displayText;

    public void OpenDoor()
    {
        _exitDoor.SetActive(false);
        _displayText.text = "Exit Opened!";
    }
}
