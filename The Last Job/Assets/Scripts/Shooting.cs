using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public TypeOfCameras typeOfCameras;

    [SerializeField]
    private ParticleSystem flashes;
    public GameObject FlashEffect;

    public Animator anim;
    public AudioSource bodyFalling;

    public float damage = 100f;
    public float range = 100f;
    public Camera fps;
    public GameObject SPoint;
    



    // Start is called before the first frame update
    void Start()
    {
        typeOfCameras = FindObjectOfType<TypeOfCameras>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(fps.transform.position, fps.transform.forward, Color.red);
        //Debug.DrawRay(SPoint.transform.position, SPoint.transform.right, Color.green);
        if (Input.GetMouseButtonDown(0) && typeOfCameras.taserOnHand == true)
        {
            Fire();
        }
    }

    public void Fire()
    {
        flashes.Play();
        RaycastHit hit;
        
        if (Physics.Raycast(SPoint.transform.position, SPoint.transform.right, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Debug.Log(hit.point);

          
            if (/*enemy != null*/ hit.transform.GetComponent<Enemy>() && Input.GetMouseButtonDown(0))
            {
             
                enemy.Damage(damage);
               // anim.Play("EMorreu");
               // bodyFalling.Play();
                Debug.Log("Morreu");
               
                GameObject impactFlare = Instantiate(FlashEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactFlare, 1f);
            }
            else
            {
                GameObject impactFlare = Instantiate(FlashEffect, hit.point, Quaternion.LookRotation(hit.normal));
                 Destroy(impactFlare, 1f);
            }   



        }
    }
}
