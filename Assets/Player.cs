using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public SphereCollider handCollider;
    bool isColliding = true;
    public GameObject colliderGameobject;
    public InputActionReference talk;
    public InputActionReference endDialogue;
    public InputActionReference sprint;
    public float sprintSpeed;
    public float walkSpeed;
    public UnityEvent closeDialogue;

    ActionBasedContinuousMoveProvider moveProvider; //reference to our MoveProvider used for sprint speed

    private void Awake()
    {
        moveProvider = this.GetComponent<ActionBasedContinuousMoveProvider>();
    }

    public void OnEnable()
    {
        talk.action.started += Talk;
        endDialogue.action.started += EndTalk;
        sprint.action.started += Sprint;
        sprint.action.canceled += Slow;
    }
    public void OnInteract()
    {
        if (isColliding)
        {
            if (colliderGameobject != null) { colliderGameobject.GetComponent<Person>().EngageDialogue(this.gameObject); }
        }
    }

    public void OnDestroy()
    {
        talk.action.started -= Talk;
        endDialogue.action.started -= EndTalk;
        sprint.action.started -= Sprint;
        sprint.action.canceled -= Slow;
    }
    private void Talk(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
        if (isColliding)
        {
            Debug.Log("IsCollidingAndInteracted");
            if (colliderGameobject != null) { colliderGameobject.GetComponent<Person>().EngageDialogue(this.gameObject); Debug.Log("EngagedDialogue"); }
        }
    }

    private void EndTalk(InputAction.CallbackContext context)
    {
        closeDialogue.Invoke();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprinting");
        moveProvider.moveSpeed = sprintSpeed;
    }

    private void Slow(InputAction.CallbackContext context)
    {
        Debug.Log("Slowing Down");
        moveProvider.moveSpeed = walkSpeed;
    }



    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        colliderGameobject = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        colliderGameobject = null;
    }

    public IEnumerator EndSprint()
    {
        var controller = GetComponent<CharacterController>();

        yield return new WaitUntil(() => controller.velocity.magnitude <= 0.2f);
        moveProvider.moveSpeed = walkSpeed;
    }
}
