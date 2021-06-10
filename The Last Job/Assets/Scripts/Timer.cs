using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 10f;

    [SerializeField] Text countdownTest;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    public void Update()
    {

        currentTime -= 1 * Time.deltaTime;
        countdownTest.text = currentTime.ToString();
    }
}
