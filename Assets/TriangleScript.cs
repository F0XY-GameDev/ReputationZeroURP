using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriangleScript : MonoBehaviour
{
    public UnityEvent OnStart;

    private void Start()
    {
        OnStart.Invoke();
    }
}
