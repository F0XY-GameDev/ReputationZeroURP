using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SphereCollider handCollider;
    bool isColliding;
    public GameObject colliderGameobject;
    public void OnInteract()
    {
        if (isColliding)
        {
            if (colliderGameobject != null) { colliderGameobject.GetComponent<Person>().EngageDialogue(this.gameObject); }
        }
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
}
