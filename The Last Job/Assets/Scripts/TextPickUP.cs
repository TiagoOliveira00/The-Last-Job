using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPickUP : MonoBehaviour
{
    public Text text;
    private GameObject player;
    public float distance = 3;
    public GameObject image;
    private bool hide = false;

    private void Start()
    {
        text.enabled = false;
        image.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.I))
            {
                hide = !hide;

                if (!hide)
                {
                    image.SetActive(true);
                }
                else
                {
                    image.SetActive(false);
                }

            }
        }
        else
        {
            text.enabled = false;
            image.SetActive(false);
        }
    }
}