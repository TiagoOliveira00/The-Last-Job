using TMPro;
using UnityEngine;

public class DoorPassword : MonoBehaviour
{
    PlayerCam pcam;

    public bool isOpen = false;
    public TMP_Text textInput;
    public string typedPassword;
    public string correctPassword;
    public GameObject painel;
    private int maxAttempts = 3;//numero tentativas maximas parar inserir código
    public AudioSource audioSource, wrongCodeAudio, correctKeyAudio;
    public LevelChanger levelChanger;
    public TMP_Text codeText;

    public void Start()
    {
        pcam = FindObjectOfType<PlayerCam>();
        codeText.enabled = false;
    }

    public void AddNumber(int num)
    {
        typedPassword += num.ToString();
        textInput.text = typedPassword;
    }

    //butao X
    public void CancelInput()
    {
        typedPassword = string.Empty;
        textInput.text = string.Empty;
    }

    //butao OK
    public void CheckTypedCode()
    {
        if (typedPassword == correctPassword)
        {
            correctKeyAudio.Play();
            painel.SetActive(false);
            pcam.isMoving = true;

            DoorManager manager = FindObjectOfType<DoorManager>(); //Encontra a referencia do Doormanager, que contem a referencia da door atual+animator
            if (manager.currentDoor != null)// Se for diferente de null, existe uma door na minha frente
            {
                manager.currentDoor.ChangeDoorState();//Mudo o estado da door abrir>fechar fechar/abrir
            }
        }
        if (typedPassword != correctPassword)
        {
            maxAttempts = maxAttempts - 1;
            wrongCodeAudio.Play();
            codeText.enabled = true;

        }
        if (maxAttempts == 0)
        {
            audioSource.Play();
            levelChanger.FadeToLevel();
        }
    }
}
