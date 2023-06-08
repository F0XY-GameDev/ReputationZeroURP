using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New sound", menuName ="Audio/Sound")] //makes it possible to create sounds in the hierarchy
public class Sound : ScriptableObject
{
    //give the Sound a name and be able to select a clip
    public string Id;
    public AudioClip Clip;

    [Range(0f,1f)] //set the range of the slider
    public float Volume = 0.25f; //create a volume slider and set the default volume

    //same like volume, not neccessary but nice to have
    [Range(0.1f,3)]
    public float Pitch = 1f;

    //select if you want the sound to loop
    public bool Loop;

    //make a public audio source so we can reference it in other scripts
    [HideInInspector]
    public AudioSource source;
}
