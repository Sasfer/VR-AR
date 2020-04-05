using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    float speed;
    bool inicio, fin, letIni, letFin, movimiento;

    public CharacterController controller;
    public TextMesh time, life, weapon, enemy;
    public float municion, vida, tiempo, enemigo;
    public ParticleSystem sal, ren;
    public GameObject letreroInicio, letreroPerder, letreroGanar, 
    letreroMorir, letreroSalir, letreroReiniciar, salir, reiniciar; 

    // Se da un timepo de espera para visualisar algun letrero
    IEnumerator Espera(GameObject letrero){
        letrero.SetActive(true);
        yield return new WaitForSeconds(10.0f);
    }

    void Start(){
        speed = 10f;

        municion = 10f;
        vida = 5f;
        enemigo = 0f;
        tiempo = 120f;

        weapon.text = "" + municion.ToString("f0");
        life.text = "" + vida.ToString("f0");
        enemy.text = "" + enemigo.ToString("f0");
        time.text = "" + tiempo.ToString("f0");

        inicio = false;
        fin = false;
        letIni = true;
        letFin = true;
        movimiento = true;

        FindObjectOfType<audio>().Stop("fondo");
        FindObjectOfType<audio>().Play("fondo");

    }

    void Update(){
    	// Se controla el movimiento del player
    	// en caso de estar en un tiempo de espera
        if(movimiento == true){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 mover = transform.right * x + transform.forward * z;
            mover.y = 0f;
            controller.Move(mover * speed * Time.deltaTime);
        }

        // Control del cronometro
        if(tiempo > 0f){
            if(inicio == true && fin == false){
                tiempo -= Time.deltaTime;
                time.text = "" + tiempo.ToString("f0"); 
            }
        }
        else{
            movimiento = false;
            StartCoroutine(Espera(letreroPerder));
            SceneManager.LoadScene("menuLaberinto");
        }
    }

    void OnTriggerEnter(Collider objeto){
    	switch(objeto.tag){
    		case "salir":
            	movimiento = false;
            	StartCoroutine(Espera(letreroSalir));
            	Application.Quit();
    			break;
    		
    		case "reiniciar":
            	movimiento = false;
            	StartCoroutine(Espera(letreroReiniciar));
            	SceneManager.LoadScene("laberinto3D");
    			break;

    		case "enemy":
    			if(vida > 1f){
                	vida -= 1.0f;
                	life.text = "" + vida.ToString("f0"); 
            	}
            	else{
                	vida -= 1.0f;
                	life.text = "" + vida.ToString("f0");
                	movimiento = false;
                	StartCoroutine(Espera(letreroMorir));
                	SceneManager.LoadScene("menuLaberinto"); 
           	 	}
    			break;

    		case "municion":
    			municion = float.Parse(weapon.text);
    			municion += 1.0f;
            	weapon.text = "" + municion.ToString("f0");
            	FindObjectOfType<audio>().Play("municion");
            	objeto.gameObject.SetActive(false); 
    			break;

    		case "bomba":
    			if(vida > 1f){
                	vida -= 1.0f;
                	life.text = "" + vida.ToString("f0");
                	FindObjectOfType<audio>().Play("bomba");
                	objeto.gameObject.SetActive(false); 
            	}
            	else{
                	vida -= 1.0f;
                	life.text = "" + vida.ToString("f0");
                	FindObjectOfType<audio>().Play("bomba");
                	objeto.gameObject.SetActive(false); 
                	movimiento = false;
                	StartCoroutine(Espera(letreroMorir));
                	SceneManager.LoadScene("menuLaberinto"); 
           	 	}
    			break;

    		case "vida":
    			vida += 1.0f;
            	life.text = "" + vida.ToString("f0");
            	FindObjectOfType<audio>().Play("vida");
            	objeto.gameObject.SetActive(false); 
    			break;

    		case "inicio":
            	inicio = true;

	            letreroPerder.SetActive(false);
	            letreroMorir.SetActive(false);
	            letreroSalir.SetActive(false);
	            letreroReiniciar.SetActive(false);
	            letreroInicio.SetActive(false);
	            letreroGanar.SetActive(false);
	            salir.SetActive(false);
           		reiniciar.SetActive(false);

	            if (letIni == true)
	                letreroInicio.SetActive(true);
    			break;

    		case "begin":
    			letIni = false;
            	letreroInicio.SetActive(false);
    			break;

    		case "fin":
    			fin = true;
            	if (letFin == true)
                	letreroGanar.SetActive(true);
    			break;

    		case "end":
    			fin = true;
           		letFin = false;
           		salir.SetActive(true);
           		reiniciar.SetActive(true);
           		sal.Play();
           		ren.Play();
            	letreroGanar.SetActive(false);
    			break;

    		case "piedras":
    			FindObjectOfType<audio>().Play("fondo");
            	FindObjectOfType<audio>().Pause("tierra");
            	FindObjectOfType<audio>().Pause("agua");
            	FindObjectOfType<audio>().Pause("bosque");
    			break;

    		case "bosque":
    			FindObjectOfType<audio>().Play("bosque");
            	FindObjectOfType<audio>().Pause("tierra");
            	FindObjectOfType<audio>().Pause("agua");
            	FindObjectOfType<audio>().Pause("fondo");
    			break;

    		case "agua":
    			FindObjectOfType<audio>().Play("agua");
	            FindObjectOfType<audio>().Pause("fondo");
	            FindObjectOfType<audio>().Pause("tierra");
	            FindObjectOfType<audio>().Pause("bosque");
    			break;

    		case "tierra":
    			FindObjectOfType<audio>().Play("tierra");
	            FindObjectOfType<audio>().Pause("fondo");
	            FindObjectOfType<audio>().Pause("agua");
	            FindObjectOfType<audio>().Pause("bosque");
    			break;

    		default:
    			break;
    	}

    }
}
