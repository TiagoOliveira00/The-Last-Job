using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    PlayerCam pcam;

    public int range = 3;
    public Vector3 offset;
    public DoorPassword doorPassword;
    public GameObject canvas;
    public Door currentDoor;

    void Start()
    {
        pcam = FindObjectOfType<PlayerCam>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDoor();
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentDoor != null)
            {
                if (currentDoor.state)
                {
                    currentDoor.ChangeDoorState();//fecha!
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Fecha/abre door");
            if (currentDoor != null)
            {

                currentDoor.ChangeDoorState();//abre!
            }
        }
    }

    public void CheckDoor()//deteta door
    {
        Ray ray = new Ray();
        ray.origin = transform.position + offset;
        ray.direction = transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            print(hit.transform.name);
            if (hit.transform.GetComponent<Door>() && Input.GetKeyDown(KeyCode.P))
            {
                //nao entra aqui
                currentDoor = hit.transform.GetComponent<Door>();
                //Ativa o painel da UI
                pcam.isMoving = false;
                canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; Debug.Log("entrou");
                doorPassword.correctPassword = currentDoor.correctKey;
                doorPassword.CancelInput();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                canvas.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + offset, transform.forward * range);
    }
}
