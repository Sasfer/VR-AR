using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma : MonoBehaviour{

	// Daño
	public float damage = 10f;
	// Rango del arma
	public float weaponRange = 50f;
	// Velocidad de disparo
	public float fireRate = 0.25f;
	// Fuerza de impacto
	public float hitForce = 0.5f;
	// Marcador para el disparo del arma
	public Transform gunEnd;
	// Munición con la que cuenta el arma
	public float municion;

	public Camera fpsCamera;
	public ParticleSystem disparo;
	public GameObject impactoDisparo;
	public TextMesh weapon;
	
	// Duración visible del disparo
	private WaitForSeconds shootDuration = new WaitForSeconds(0.07f);
	// Línea visible del disparo
	private LineRenderer laserLine;
	// Proximo disparo
	private float nextFire;


	void Start(){
		laserLine = GetComponent<LineRenderer>();
	}

    void Update(){
    	// Detección del disparo
		if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;

			municion = float.Parse(weapon.text);

			if(municion > 0f){
				municion = municion - 1f;
	    		weapon.text = "" + municion.ToString("f0");

				StartCoroutine(ShootEffect());

				Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0f));
				RaycastHit hit;

				// Determinación de posiciones iniciales y finales del disparo
				laserLine.SetPosition(0, gunEnd.position);

				// Detección de colisión
				if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, weaponRange)){
					laserLine.SetPosition(1, hit.point);

					target vida = hit.collider.GetComponent<target>();

					if(vida != null){
						vida.TakeDamage(damage);
					}

					if(hit.rigidbody != null){
						hit.rigidbody.AddForce(-hit.normal * hitForce);
					}
				}
				else{
					laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * weaponRange));
				}

				GameObject impacto = Instantiate(impactoDisparo, hit.point, Quaternion.LookRotation(hit.normal));
	    		Destroy(impacto, 2f);
			}
		}        
    }

    private IEnumerator ShootEffect(){
    	FindObjectOfType<audio>().Play("disparo");
    	disparo.Play();

    	laserLine.enabled = true;

    	yield return shootDuration;

    	laserLine.enabled = false;
    }
}
