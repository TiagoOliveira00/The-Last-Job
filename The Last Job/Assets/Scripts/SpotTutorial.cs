using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTutorial : MonoBehaviour
{
    public string spotName;
    public DialogManager dialogManager;
    public List<string> spotConvo = new List<string>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogManager.StarDialog(spotName, spotConvo);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        dialogManager.StopDialog();

    }
}
