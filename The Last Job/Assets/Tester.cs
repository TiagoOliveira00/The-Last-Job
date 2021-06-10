using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public GameObject barreira1, barreira2;
    void Start()
    {
        barreira1.SetActive(true);
        barreira2.SetActive(true);
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Pronto pra desativar barreiras");
            barreira1.SetActive(false);
            barreira2.SetActive(false);
        }
    }
}
