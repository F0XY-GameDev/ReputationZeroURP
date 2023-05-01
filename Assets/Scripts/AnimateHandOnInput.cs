using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction; //create variable and mark it public to control it in the Unity Editor
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator; //create animator

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>(); //check if the button is pressed, stored in a local variable; float because we check how much the trigger/button is pressed
        handAnimator.SetFloat("Trigger", triggerValue); //set the value

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
