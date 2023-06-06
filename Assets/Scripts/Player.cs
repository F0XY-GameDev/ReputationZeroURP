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
    public InputActionReference walk;
    public float sprintSpeed;
    public float walkSpeed;
    public UnityEvent closeDialogue;
    public UnityEvent onStartMove;
    public UnityEvent onStopMove;
    [SerializeField] AudioSource footstepsSource;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip running;

    public bool isTalking;

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
        walk.action.started += Walk;
        walk.action.canceled += EndWalk;
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
        if (isTalking)
        {
            return;
        }
        Debug.Log("Interacted");
        if (isColliding)
        {
            Debug.Log("IsCollidingAndInteracted");
            if (colliderGameobject != null) { colliderGameobject.GetComponent<Person>().EngageDialogue(this.gameObject); Debug.Log("EngagedDialogue"); }
        }
    }
    void Walk(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        if (inputValue != Vector2.zero)
        {
            onStartMove.Invoke();
        }
    }
    void EndWalk(InputAction.CallbackContext context)
    {
        onStopMove.Invoke();
    }
    void EndTalk(InputAction.CallbackContext context)
    {
        Debug.Log("Dialogue Ended");
        closeDialogue.Invoke();        
    }

    void Sprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprinting");
        moveProvider.moveSpeed = sprintSpeed;
        footstepsSource.clip = running;
    }

    private void Slow(InputAction.CallbackContext context)
    {
        Debug.Log("Slowing Down");
        moveProvider.moveSpeed = walkSpeed;
        footstepsSource.clip = walking;
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
