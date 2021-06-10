using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100f;
    public Animator anim;
     public AudioSource scream;

    public void Start()
    {
        
    }
    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        //  Destroy(gameObject);
        anim.Play("EMorreu");
        scream.Play();
       // bodyGuard.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = false;
    }

}
