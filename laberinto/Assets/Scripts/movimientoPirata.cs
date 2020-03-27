using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movimientoPirata : MonoBehaviour
{
    float speed;

    public float municion, vida, tiempo, enemigo;
    public CharacterController controller;
    public TextMesh time, life, weapon, enemy;
    public GameObject letreroInicio, letreroPerder, letreroGanar, 
    letreroMorir, letreroOpciones, letreroSalir, player; 

    bool inicio, fin, letIni, letFin, movimiento;

    // Start is called before the first frame update
    void Start()
    {
        speed = 12f;
        municion = 1f;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(movimiento == true){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 mover = transform.right * x + transform.forward * z;

            controller.Move(mover * speed * Time.deltaTime);
        }

        if(tiempo > 0f){
            if(inicio == true && fin == false){
                tiempo -= Time.deltaTime;
                time.text = "" + tiempo.ToString("f0"); 
            }
        }
        else{
            movimiento = false;
            letreroPerder.SetActive(true);
            StartCoroutine(Espera());
        }
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(4f);
        FindObjectOfType<audio>().Stop("fondo");
        SceneManager.LoadScene("menuLaberinto");
    }

    void OnTriggerEnter(Collider objeto)
    {
        if (objeto.tag == "opciones"){
            letreroOpciones.SetActive(true);
            movimiento = false;
            StartCoroutine(Espera());
        }

        if (objeto.tag == "salir"){
            letreroSalir.SetActive(true);
            movimiento = false;
            Application.Quit();
        }

        if (objeto.tag == "municion")
        {
            municion += 1.0f;
            weapon.text = "" + municion.ToString("f0");
            FindObjectOfType<audio>().Play("municion");
            objeto.gameObject.SetActive(false);        
        }

        if (objeto.tag == "bomba")
        {
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
                letreroMorir.SetActive(true);
                StartCoroutine(Espera()); 
            }
        }

        if(objeto.tag == "vida")
        {
            vida += 1.0f;
            life.text = "" + vida.ToString("f0");
            FindObjectOfType<audio>().Play("vida");
        }

        if(objeto.tag == "inicio1")
        {
            FindObjectOfType<audio>().Play("fondo");
            FindObjectOfType<audio>().Pause("desierto");
            FindObjectOfType<audio>().Pause("lago");
            FindObjectOfType<audio>().Pause("bosque");
            FindObjectOfType<audio>().Pause("menu");

            inicio = true;

            letreroPerder.SetActive(false);
            letreroMorir.SetActive(false);
            letreroOpciones.SetActive(false);
            letreroSalir.SetActive(false);

            if (letIni == true)
                letreroInicio.SetActive(true);

        }

        if(objeto.tag == "inicio2")
        {
            letIni = false;
            letreroInicio.SetActive(false);
            
        }

        if(objeto.tag == "fin1")
        {

            fin = true;
            if (letFin == true)
                letreroGanar.SetActive(true);
        }

        if(objeto.tag == "fin2")
        {
            fin = true;
            letFin = false;
            letreroGanar.SetActive(false);

        }

        if(objeto.tag == "arena")
        {
            FindObjectOfType<audio>().Play("desierto");
            FindObjectOfType<audio>().Pause("fondo");
            FindObjectOfType<audio>().Pause("lago");
            FindObjectOfType<audio>().Pause("bosque");
        }

        if(objeto.tag == "bosque")
        {
            FindObjectOfType<audio>().Play("bosque");
            FindObjectOfType<audio>().Pause("desierto");
            FindObjectOfType<audio>().Pause("lago");
            FindObjectOfType<audio>().Pause("fondo");
        }

        if(objeto.tag == "agua")
        {
            FindObjectOfType<audio>().Play("lago");
            FindObjectOfType<audio>().Pause("fondo");
            FindObjectOfType<audio>().Pause("desierto");
            FindObjectOfType<audio>().Pause("bosque");
        }
    }
}
