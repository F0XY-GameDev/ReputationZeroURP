using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//by adding XRGrabInteractable instead of the MonoBehaviour we create a script that extends the already existing script
public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachedTransform;
    public Transform rightAttachedTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachedTransform;
        }
        else if(args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachedTransform;
        }

        base.OnSelectEntered(args);
    }
}
