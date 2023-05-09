using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public SphereCollider handCollider;
    bool isColliding = true;
    public GameObject colliderGameobject;
    public InputActionReference controls;

    public void OnEnable()
    {
        controls.action.started += Interact;
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
        controls.action.started -= Interact;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
        if (isColliding)
        {
            Debug.Log("IsCollidingAndInteracted");
            if (colliderGameobject != null) { colliderGameobject.GetComponent<Person>().EngageDialogue(this.gameObject); Debug.Log("EngagedDialogue"); }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        colliderGameobject = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        
        colliderGameobject = null;
    }
}
