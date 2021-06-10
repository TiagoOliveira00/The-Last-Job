using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Movimento Base")]
    public Transform foco;
    public float sensibility = 150.0f;
    public float velocidade = 120.0f;
    public float verticalLimit =   70.0f;
    private Vector2 rot;
    private Vector2 mouse;
    private Vector2 camInput;
    [Space]
    [Header("Colisão Camera")]
    public LayerMask mask;
    public float minDist = 0.6f;
    public float maxDist = 2.5f;
    private float distance;
    private Vector3 localDir;
    private Transform cameraBase;
    private Camera cam;

    public bool isMoving = true;

    #region Inicialização
    private void Start()
    {
        cam = GetComponent<Camera>();
        cameraBase = transform.parent;
        localDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    private void Update()
    {
        if (isMoving == true)
        {
            mouse.x = Input.GetAxis("Mouse X");
            mouse.y = -Input.GetAxis("Mouse Y");
            camInput.x = mouse.x;// + joystick
            camInput.y = mouse.y;// + joystick
            rot.y += camInput.x * sensibility * Time.deltaTime;
            rot.x += camInput.y * sensibility * Time.deltaTime;
            rot.x = Mathf.Clamp(rot.x, -verticalLimit * 0.6f, verticalLimit);
            Quaternion locRot = Quaternion.Euler(rot.x, rot.y, 0);
            cameraBase.transform.rotation = locRot;
        }
        else if (isMoving == false)
        {

        }
 
    }

    private void LateUpdate()
    {
        cameraBase.transform.position = Vector3.MoveTowards(cameraBase.transform.position, foco.position, velocidade * Time.deltaTime);

        Vector3 origin = cameraBase.transform.TransformPoint(localDir * (maxDist + 0.5f));
        Vector3 cross = Vector3.Cross(origin.normalized, cameraBase.transform.TransformDirection(transform.up))* 2;
        distance = float.MaxValue;
        bool collision = false;
        RaycastHit hit;

        if (Physics.Linecast(cameraBase.transform.position, origin, out hit, mask))
        {
            collision = true;
            if (hit.distance < distance)
                distance = hit.distance;
        }
        if (Physics.Linecast(cameraBase.transform.position, origin + cross + transform.up, out hit, mask))
        {
            collision = true;
            if (hit.distance < distance)
                distance = hit.distance;
        }
        if (Physics.Linecast(cameraBase.transform.position, origin - cross + transform.up, out hit, mask))
        {
            collision = true;
            if (hit.distance < distance)
                distance = hit.distance;
        }
        if (Physics.Linecast(cameraBase.transform.position, origin + cross - transform.up, out hit, mask))
        {
            collision = true;
            if (hit.distance < distance)
                distance = hit.distance;
        }
        if (Physics.Linecast(cameraBase.transform.position, origin - cross - transform.up, out hit, mask))
        {
            collision = true;
            if (hit.distance < distance)
                distance = hit.distance;
        }
        distance -= 0.2f;
        distance = collision ? Mathf.Clamp(distance, minDist, maxDist) : maxDist;
        cam.nearClipPlane = distance < 1.0f ? 0.01f : 0.5f;
        transform.localPosition = Vector3.Lerp(transform.localPosition, localDir * distance, Time.deltaTime * 20);
    }
}