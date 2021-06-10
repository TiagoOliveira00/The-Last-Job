using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    public LevelChanger levelChanger;
    public CameraOn_OFF camOff;

    [SerializeField]
    private Color test = Color.green;
    private Light lightColor;
    private float detention;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    public GameObject sprite;
    public Image image;

    public bool reduceBar = false;

    [SerializeField]
    private LayerMask layer;

    public void Start()
    {
        camOff = FindObjectOfType<CameraOn_OFF>();
        lightColor = GetComponent<Light>();
    }

    public void Update()
    {
        CamOff();

        image.fillAmount = detention / 100;

        if (detention == 100)
        {
            Debug.Log("game over");
            levelChanger.FadeToLevel();
        }
        if (reduceBar)
        {
            detention = 0;
            reduceBar = false;
        }
    }

    public void CamOff()
    {
        if (camOff.isAbove == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerInteractionZone")
        {
            // sprite.SetActive(!sprite);
            this.GetComponent<Renderer>().material.color = test;
            this.lightColor.color = test;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            detention += Time.deltaTime * speed;
            Debug.Log(Time.deltaTime);
            Debug.Log(detention);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "PlayerInteractionZone")
        {
            // sprite.SetActive(!sprite);
            reduceBar = true;
            this.GetComponent<Renderer>().material.color = Color.red;
            this.lightColor.color = Color.red;
        }
    }
}