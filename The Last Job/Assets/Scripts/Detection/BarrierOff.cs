using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierOff : MonoBehaviour
{
    GameObject barrier;

    public void Update()
    {
        barrier.gameObject.SetActive(false);
    }
}
