using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashLight : MonoBehaviour
{

    public Light light;


    private void Start()
    {
        light.enabled=false;
    }
    void Update() 
    {

        if (Input.GetKeyDown(KeyCode.M))
        {

            if (light.enabled == false)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }

        }
    }
    
}