using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSound : MonoBehaviour
{
    [SerializeField] private Sound _audiocue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play(_audiocue.Id);
        }
    }
}
