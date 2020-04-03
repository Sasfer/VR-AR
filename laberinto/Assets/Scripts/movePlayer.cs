using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    float speed;
    public CharacterController controller;

    void Start()
    {
        speed = 12f;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        controller.Move(mover * speed * Time.deltaTime);
    }
}
