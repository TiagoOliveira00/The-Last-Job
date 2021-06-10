using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaFrigorifico : MonoBehaviour
{
    public Animator anim;
    public bool canOpen = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone" && Input.GetKeyDown(KeyCode.L) && canOpen == true)
        {
            anim.Play("Abrir");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            anim.Play("Fechar");
        }
    }

}
