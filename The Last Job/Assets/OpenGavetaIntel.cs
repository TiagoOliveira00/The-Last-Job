using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGavetaIntel : MonoBehaviour
{


    public GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
        }
    }
}
