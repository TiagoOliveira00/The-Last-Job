using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class LastGame : MonoBehaviour
{
    public LevelChanger levelChanger;
    public PortaFrigorifico pf;

    LightingSettings ls;
    RenderSettings rs;

    [SerializeField] Text countdownTest;
    [SerializeField] GameObject button1;

    private float currentTime = 0f;
    private float startingTime = 90f;

    public bool timerOn = false;
    private bool lightOn;

    public void Start()
    {
        currentTime = startingTime;
    }
    public void Update()
    {
        if (timerOn == true)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownTest.text = currentTime.ToString("0");

            RenderSettings.ambientLight = Color.black;
            RenderSettings.ambientIntensity = 0.01f;
            RenderSettings.ambientSkyColor = Color.black;
            RenderSettings.ambientGroundColor = Color.black;
            RenderSettings.reflectionIntensity = 0.01f;
            lightOn = false;
            pf.canOpen = false;

            if (lightOn == true)
            {
                RenderSettings.ambientLight = new Color(71, 84, 91, 0);
                RenderSettings.ambientIntensity = 1f;
                RenderSettings.ambientSkyColor = Color.cyan;
                RenderSettings.ambientGroundColor = Color.cyan;
                RenderSettings.reflectionIntensity = 1;

                pf.canOpen = true;
            }
        }

        if (currentTime <= 0)
        {
            countdownTest.gameObject.SetActive(false);
            levelChanger.FadeToLevel();
        }

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerInteractionZone")
        {
            timerOn = true;
            countdownTest.gameObject.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider c)
    {
        if (c.tag == "PlayerInteractionZone" && Input.GetKeyDown(KeyCode.E))
        {
            lightOn = true;
        }
    }
}
