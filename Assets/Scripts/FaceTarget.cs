using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    // GPT Generated Script    

    [SerializeField] Transform target; // The target to face

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Ignore any height difference
            direction.Normalize();

            // Calculate the rotation to face the target
            Quaternion targetRotation = Quaternion.LookRotation(-direction);

            // Apply the rotation to the object
            transform.rotation = targetRotation;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
