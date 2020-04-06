using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour{
	public TextMesh enemy;

	public float enemigo;
	public float health = 20f;

	public GameObject santaW, santaD;

	private WaitForSeconds espera = new WaitForSeconds(1f);

	public void TakeDamage (float amount){
		health -= amount;

		if(health <= 0f){
			enemigo = float.Parse(enemy.text);
			enemigo = enemigo + 1f;
			enemy.text = "" + enemigo.ToString("f0");
			santaW.SetActive(false);
			santaD.SetActive(true);
			StartCoroutine(Wait());
		}
	}

	private IEnumerator Wait(){
    	yield return espera;
    	gameObject.SetActive(false);
    }
}
