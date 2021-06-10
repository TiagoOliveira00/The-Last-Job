using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cam;
    private CharacterController controller;
    private Animator anim;
    public GameObject camera3P, camera1P;
    private Interaction interaction;
    public TypeOfCameras typeOfCameras;
    public GameObject taserMesh;

    private Vector3 gravity;
    private Vector2 dirController;

    private float speed;
    private float crouched;
    public float jump = 1f;
    private const float G = -9.81f;

    public float inputCrouch;

    public bool isMoving = true;
    public bool isCrouched = false; 
    private bool typing;

    public AudioSource audioPunch;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }

    private void OnTriggerEnter(Collider other)
    {
        Interaction inter = other.GetComponent<Interaction>();
        if (inter != null)
        {
            interaction = inter;
        }

    }

    private void Update()
    {
        dirController.x = Input.GetAxisRaw("Horizontal");
        dirController.y = Input.GetAxisRaw("Vertical") / 2;
        float inputRun = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

        //AGACHAR
        if (Input.GetKey(KeyCode.LeftControl))
        {
            inputCrouch = 1;
            isCrouched = true;
        }
        else
        {
            inputCrouch = 0;
            isCrouched = false;
        }

        anim.SetBool("itemchao", false);
        anim.SetBool("itemmesa", false);

        //APANHAR ITEM
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interaction != null && Vector3.Distance(transform.position, interaction.transform.position) < 10)
            {
                if (interaction.local == Local.floor)
                {
                    anim.SetBool("itemchao", true);
                }
                else if (interaction.local == Local.table)
                {
                    anim.SetBool("itemmesa", true);
                }
                else if (interaction.local == Local.door)
                {
                    anim.SetBool("digitandosenha", true);
                    typing = true;
                }
            }
        }

        //SOCO
        if (Input.GetMouseButton(2))
        {
            anim.Play("Murro");
            //audioPunch.Play();
        }

       

        //droppar objetos
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if ((/*interaction != null && Vector3.Distance(transform.position, interaction.transform.position) < 10) &&*/ typeOfCameras.taserOnHand)
            if (interaction != null && Vector3.Distance(transform.position, interaction.transform.position) < 10 && typeOfCameras.taserOnHand)
            {

                anim.SetBool("itemchao", true);

            }
            
        }
        //COLOCAR SENHA
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            anim.SetBool("digitandosenha", false);
            typing = false;
        }

        //TIPO DE CAMERAS 1 E  3 PESSOA
        if (typeOfCameras.taserOnHand) //Caso possua o taser, mudo pra terceira/primeira pessoa se estiver apertando o botão de apontar
        {
            if (!taserMesh.activeSelf)
            {
                taserMesh.SetActive(true);
            }
           
            camera1P.SetActive(Input.GetMouseButton(1));
            camera3P.SetActive(!Input.GetMouseButton(1));

        }
        else //Se não tiver, uso apenas a terceira pessoa;
        {
            camera3P.SetActive(true);
            if (taserMesh.activeSelf)
            {
                taserMesh.SetActive(false);
            }

            if (camera1P.activeSelf)
            {
                camera1P.SetActive(false);
            }
        }

        if (inputCrouch > 0.1f && Mathf.Abs(dirController.y) > 0.1f)
        {
            inputRun = 2;
        }

        crouched = Mathf.MoveTowards(crouched, inputCrouch, 4f * Time.deltaTime);
        speed = Mathf.MoveTowards(speed, Mathf.Abs(dirController.y) * inputRun, 1.5f * Time.deltaTime);

        anim.SetFloat("velocidade", speed);
        anim.SetFloat("agachado", crouched);

        dirController = dirController.normalized;

        float anguloRotacao = Mathf.Atan2(dirController.x, dirController.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        Quaternion rotacao = Quaternion.Euler(0, anguloRotacao, 0);
        Quaternion rotacaoFinal = Quaternion.Slerp(transform.rotation, rotacao, 5 * Time.deltaTime);

        if (dirController.magnitude != 0 && !typing)
        {
            controller.transform.rotation = rotacaoFinal;
            //controller.Move(transform.forward * Time.deltaTime);
        }
        gravity.y += G * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

   
}