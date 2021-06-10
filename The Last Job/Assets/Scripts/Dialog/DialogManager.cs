using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI spotNameText;
    public TextMeshProUGUI dialogText;

    private List<string> conversation;
    private int convoIndex;



    void Start()
    {
        dialogPanel.SetActive(false);
    }

   public void StarDialog(string _spotName, List<string> _convo)
    {
        Cursor.visible = true;
        spotNameText.text = _spotName;
        conversation = new List<string>(_convo);
        dialogPanel.SetActive(true);
        convoIndex = 0;
        ShowText();
    }

    public void StopDialog()
    {
        dialogPanel.SetActive(false);
        Cursor.visible = false;
    }

    private void ShowText()
    {
        dialogText.text = conversation[convoIndex];
    }

    public void Next()
    {
        if (convoIndex < conversation.Count-1)
        {
            convoIndex += 1;// faz aparecer os textos seguintes que estão listados
            ShowText();
        }

        
    }
}
