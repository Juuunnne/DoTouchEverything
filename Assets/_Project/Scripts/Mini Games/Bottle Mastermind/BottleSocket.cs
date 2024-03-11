using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BottleSocket : MonoBehaviour
{
    [SerializeField]
    private XRSocketInteractor _socketInteractor;

    public int WantedBottleDigit { get; set; } = 0;
    public bool IsDigitCorrect { get; private set; } = false;

    private void Awake()
    {
        _socketInteractor.selectEntered.AddListener(OnSelectEntered);
        _socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out Bottle bottle))
        {
            IsDigitCorrect = bottle.Digit == WantedBottleDigit;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        IsDigitCorrect = false;
    }

    private void OnDestroy()
    {
        _socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        _socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }
}
