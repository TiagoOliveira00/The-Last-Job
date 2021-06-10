using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string correctKey;
    public Animator anim;
    public bool state;
    private bool canUse = true;
    public AudioSource audio;

    public GameObject collider;

    public void ChangeDoorState()
    {
        if (canUse)
        {
            state = !state; //altera o estado
            if (state)
            {
                anim.Play("Abrir");
                Invoke(nameof(OpenCollider), 1.5f);
                audio.Play();
            }
            else
            {
                anim.Play("Fechar");
                Invoke(nameof(CloseCollider), 1.5f);
                audio.Play();
            }
            canUse = false;
            Invoke(nameof(Unlock), 2); // chama o unlock com dois segundos;
        }
    }

    private void OpenCollider()
    {
        collider.SetActive(false);
    }

    private void CloseCollider()
    {
        collider.SetActive(true);
    }

    private void Unlock()
    {
        canUse = true;
    }
}

