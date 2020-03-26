using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audio : MonoBehaviour{
    public sonido[] clip;

    public static audio instance;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(sonido s in clip){
            s.fuente = gameObject.AddComponent<AudioSource>();
            s.fuente.clip = s.clip;
            s.fuente.volume = s.volume;
            s.fuente.pitch = s.pitch;
            s.fuente.loop = s.loop;
        }
    }

    public void Play(string nombre){
        sonido s = Array.Find(clip, sonido => sonido.nombre == nombre);
        if (s == null){
            Debug.LogWarning("Clip " + nombre + " no encontrado D:");
            return;    
        }
        s.fuente.Play();
    }

    public void Pause(string nombre){
        sonido s = Array.Find(clip, sonido => sonido.nombre == nombre);
        if (s == null){
            Debug.LogWarning("Clip " + nombre + " no encontrado D:");
            return;
        }
        s.fuente.Pause();
    }

    public void Stop(string nombre){
        sonido s = Array.Find(clip, sonido => sonido.nombre == nombre);
        if (s == null){
            Debug.LogWarning("Clip " + nombre + " no encontrado D:");
            return;
        }
        s.fuente.Stop();
    }
}
