using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]

public class ForceGrabModifier : MonoBehaviour
{
    XRRayInteractor _interactor; 
    InputActionProperty _grabbedObjectTranslateAnchorAction;

    [SerializeField] ActionBasedController _controller;
    [SerializeField] InputActionProperty _forceGrabTranslateAnchorAction;

    private void Start()
    {
        _interactor = GetComponent<XRRayInteractor>();
        _interactor.selectEntered.AddListener(OnObjectGrab);
        _interactor.selectExited.AddListener(OnObjectRelease);
    }

    private void OnDestroy()
    {
        _interactor.selectEntered.RemoveListener(OnObjectGrab);
        _interactor.selectExited.RemoveListener(OnObjectRelease);
    }

    void OnObjectGrab(SelectEnterEventArgs a)
    {
        if (a.interactableObject.transform.TryGetComponent(out RayAttachModifier _))
        {
            _grabbedObjectTranslateAnchorAction = _controller.translateAnchorAction;
            _controller.translateAnchorAction = _forceGrabTranslateAnchorAction;
        }
    }

    void OnObjectRelease(SelectExitEventArgs a)
    {
        if (a.interactableObject.transform.TryGetComponent(out RayAttachModifier _))
        {
            _controller.translateAnchorAction = _grabbedObjectTranslateAnchorAction;
        }
    }

}
