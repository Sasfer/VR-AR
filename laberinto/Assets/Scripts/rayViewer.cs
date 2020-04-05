using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayViewer : MonoBehaviour{
    
	public float weaponRange = 50f;

	public Camera fpsCamera;

    void Start(){
        
    }

    void Update(){
        Vector3 lineOrigin = fpsCamera.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0f));
        Debug.DrawRay(lineOrigin, fpsCamera.transform.forward * weaponRange, Color.yellow);
        
    }
}
