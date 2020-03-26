using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlMenuLaberinto : MonoBehaviour{
    
    void Start(){
        FindObjectOfType<audio>().Play("menu");
    }

    public void CambiarEscena(string nombreEscena){
        print(" Cambiando a la escena " + nombreEscena);
        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir(){
        print(" Saliendo de la APK ");
        Application.Quit();
    }

    public void Click(){
    	FindObjectOfType<audio>().Play("seleccion");
    }
}
