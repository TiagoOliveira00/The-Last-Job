using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taser : MonoBehaviour
{
    public float aliveTime;
    public float damage;
    public float moveSpeed;
    public GameObject bulletSpawn;
    private GameObject enemy;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime<=0)
        {//destroi a bala apois o tempo de vida termiar
            Destroy(this.gameObject);
        }
        //isntancia a bala
        transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject;
            enemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }    
    }
}
