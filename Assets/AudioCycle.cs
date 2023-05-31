using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCycle : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioSource source;

    private void Start()
    {
        source.clip = clips[0];
        source.Play();
        StartCoroutine(PlayVariantAfterFirst());
    }

    IEnumerator PlayVariantAfterFirst()
    {
        yield return new WaitWhile(() => source.isPlaying);
        source.clip = clips[1];
        source.loop = true;
        source.Play();
    }
}
