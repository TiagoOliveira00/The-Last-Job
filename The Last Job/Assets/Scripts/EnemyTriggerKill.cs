using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerKill : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioPunch, bodyFalling;
    public GameObject trigger;
    void Start()
    {
        trigger.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && Input.GetMouseButton(2))
        {
            Debug.Log("entrou");

            audioPunch.Play();
            anim.Play("Dead");
            bodyFalling.Play();
            trigger.SetActive(false);

        }
    }
}
