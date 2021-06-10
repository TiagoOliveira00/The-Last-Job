using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuNew : MonoBehaviour
{
    public GameObject optionsMenu;
    public AudioSource music;
    public Slider musicVolumeSlider;
    public float musicVolume;

    // Start is called before the first frame update
    void Start()
    { 
        optionsMenu.SetActive(false);
    }
  

    // Update is called once per frame
    void OnEnable()
    {

        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolume(); });
    }


        public void OnMusicVolume()
    {
        music.volume = musicVolume = musicVolumeSlider.value;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Resolution1080()
    {
        Screen.SetResolution(1920, 1080,  Screen.fullScreen);
    }

    public void Resolution720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }

    public void Resolution480()
    {
        Screen.SetResolution(640, 480, Screen.fullScreen);
    }

    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
