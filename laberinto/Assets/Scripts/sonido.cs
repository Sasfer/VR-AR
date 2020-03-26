using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sonido 
{
    public string nombre;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource fuente;

    public bool loop;
}
