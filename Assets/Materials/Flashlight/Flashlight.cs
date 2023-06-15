using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Material lens;

    private Light _light;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void LightOn()
    {
        _audioSource.Play();
        lens.EnableKeyword("_EMISSION");
        _light.enabled = true;
    }

    public void LightOff()
    {
        _audioSource.Play();
        lens.DisableKeyword("_EMISSION");
        _light.enabled = false;
    }

   
}
