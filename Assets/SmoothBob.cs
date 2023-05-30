using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothBob : MonoBehaviour
{
    Vector3 originalPosition;
    [SerializeField] bool movingUp = true;
    [SerializeField] float speed = 1f;
    [SerializeField] float maxHeight = 1f;
    [SerializeField] float minHeight = -1f;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        float targetHeight = movingUp ? maxHeight : minHeight; //float is maxHeight when movingUp = true and vice versa
        Vector3 targetPosition = originalPosition + Vector3.up * targetHeight;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingUp = !movingUp;
        }
    }
}
