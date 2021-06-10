using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCam : MonoBehaviour
{
    public Transform player; // localização do player

    public float sensability = 200f;
    private float xRotation = 0f;

    private float xMouse;
    private float yMouse;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//inicio do rato no centro do ecra
        Cursor.visible = false;
    }


    void Update()
    {
        xMouse = Input.GetAxis("Mouse X") * sensability * Time.deltaTime; // Rotação do rato por segundos em x
        yMouse = Input.GetAxis("Mouse Y") * sensability * Time.deltaTime; // Rotação do rato por segundos em y 

        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);// permite "travar" a rotação quando se olhar totalmente para cima ou para baixo 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * xMouse);


    }
}
