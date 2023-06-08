using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    //create public instance to communicate with other scripts
    public static AudioManager Instance;

    public void Awake()
    {
        //check if we already have the instance. if destroy, if not create.
        if (Instance !=null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        //the name explains it but: this makes the game object audio manager not destroyed if we load another scene
        DontDestroyOnLoad(gameObject);

        //add components to all the sounds in the audio manager
        foreach (var sound in _sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.source = source;
            source.clip = sound.Clip;
            source.volume = sound.Volume;
            source.pitch = sound.Pitch;
            source.loop = sound.Loop;
        }
    }

    //make the audio manager play the sounds
    public void Play(string id)
    {
        Sound s = Array.Find(_sounds, sound => sound.Id == id);

        if (s == null)
        {
            Debug.LogWarning($"Sounds with name {id} no found");
            return;
        }

        s.source.Play();
    }

    //make the audio manager stop playing a sound
    public void Stop(string id)
    {
        Sound s = Array.Find(_sounds, sound => sound.Id == id);

        if (s == null)
        {
            Debug.LogWarning($"Sounds with name {id} no found");
            return;
        }

        s.source.Stop();
    }
}
