using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOn_OFF : MonoBehaviour
{
    public bool isAbove = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerInteractionZone")
        {
            Debug.Log("Pisou");
            isAbove = true;
        }
    }
}
