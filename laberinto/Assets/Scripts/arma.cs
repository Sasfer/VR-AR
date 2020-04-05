using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
	public float impactoFuerza = 30f;
	public float fuego = 15f;
	public float municion;

	public Camera fpsCamera;
	public ParticleSystem disparo;
	public GameObject impactoDisparo;
	public TextMesh weapon;
	
	private float proximoDisparo = 0f;

    void Update(){
		if(Input.GetButtonDown("Fire1")){
			proximoDisparo = Time.time + 1f/fuego;
			Shoot();
		}        
    }

    void Shoot(){

    	municion = float.Parse(weapon.text);

    	if(municion > 0f){
    		municion = municion - 1f;
	    	weapon.text = "" + municion.ToString("f0");
	    	disparo.Play();
	    	FindObjectOfType<audio>().Play("disparo");

	    	RaycastHit hit;

	    	if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)){
	    		Debug.Log(hit.transform.name);

	    		target target = hit.transform.GetComponent<target>();

	    		if(target != null){
	    			target.TakeDamage(damage);
	    		}
	    	}

	    	if(hit.rigidbody != null){
	    		hit.rigidbody.AddForce(-hit.normal*impactoFuerza);
	    	}

	    	GameObject impacto = Instantiate(impactoDisparo, hit.point, Quaternion.LookRotation(hit.normal));
	    	Destroy(impacto, 2f);
    	}
    }
}
