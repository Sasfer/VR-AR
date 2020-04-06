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

    public GameObject e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12;

    // Se da un timepo de espera para visualisar algun letrero
    IEnumerator Espera(GameObject letrero){
        letrero.SetActive(true);
        yield return new WaitForSeconds(10.0f);
    }

    void Start(){
        speed = 8f;

        municion = 20f;
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
        FindObjectOfType<audio>().Stop("bosque");
        FindObjectOfType<audio>().Stop("tierra");
        FindObjectOfType<audio>().Stop("agua");
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

    		case "cesped":
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

    		case "e1":
    			e1.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e2":
    			e2.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e3":
    			e3.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e4":
    			e4.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e5":
    			e5.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e6":
    			e6.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e7":
    			e7.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e8":
    			e8.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e9":
    			e9.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e10":
    			e10.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e11":
    			e11.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		case "e12":
    			e12.SetActive(true);
    			objeto.gameObject.SetActive(false); 
    			break;

    		default:
    			break;
    	}

    }
}
