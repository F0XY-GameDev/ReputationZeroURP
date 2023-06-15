using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeScript : MonoBehaviour
{
    [SerializeField] Evidence testEvidence;

    public UnityEvent OnStart;

    private void Start()
    {
        OnStart.Invoke();
    }
}
