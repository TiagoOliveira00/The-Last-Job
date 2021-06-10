using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaPrincipal : MonoBehaviour
{
    public GameObject collider;
    public bool state;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        state = !state; //altera o estado
        if (state)
        {
            anim.Play("Abrir");
            Invoke(nameof(OpenCollider), 1.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.Play("Fechar");
        Invoke(nameof(CloseCollider), 1.5f);
    }

    private void OpenCollider()
    {
        collider.SetActive(false);
    }

    private void CloseCollider()
    {
        collider.SetActive(true);
    }
}
