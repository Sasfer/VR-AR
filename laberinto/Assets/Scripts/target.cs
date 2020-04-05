using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour{
	public TextMesh enemy;

	public float enemigo;
	public float health = 20f;

	public void TakeDamage (float amount){
		health -= amount;

		if(health <= 0f){
			enemigo = float.Parse(enemy.text);
			enemigo = enemigo + 1f;
			enemy.text = "" + enemigo.ToString("f0");
			Die();
		}
	}

	void Die(){
		Destroy(gameObject);
	}
}
