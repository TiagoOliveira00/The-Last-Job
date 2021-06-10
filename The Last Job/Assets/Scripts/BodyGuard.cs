using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGuard : MonoBehaviour
{
    PlayerController p;

    public List<Transform> waypoints = new List<Transform>();

    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDist = 0.8f;
    private int lastWaypointIndex;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 4.0f;
    public int range = 3;
    public float backRange = 10;

    public Vector3 offset;
    public Transform player;
    private Transform enemyPos;
    public float angulo;
    public LevelChanger levelChanger;
    public AudioSource stopSound;
    public Animator anim, foundAnim;
    public bool isAlive = true;
    Enemy enemy;

    Vector3 targetPos;

    bool isRotating = false;

    private void Start()
    {
        p = FindObjectOfType<PlayerController>();

        lastWaypointIndex = waypoints.Count - 1;//dá quantos waypoints tem no array

        //qual o primeiro  em que waypoints vai logo em frente
        targetWaypoint = waypoints[targetWaypointIndex];
        //waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
    }

    private void Update()
    {
        if (isRotating)
        {
            Quaternion rot = Quaternion.LookRotation(p.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
        }
        else
        {
            float moveStep = moveSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            //directionTarget armazena a direção do waypoint para o inimigo
            Vector3 directionTarget = (targetWaypoint.position - transform.position).normalized;

            //com ã direção obtida em cima é psssivel saber quanto precisa rodar para estar a encarar o waypoint
            Quaternion rotaionToTarget = Quaternion.LookRotation(directionTarget, Vector3.up);

            //suaviza a rotação para o target
            transform.rotation = Quaternion.Slerp(transform.rotation, rotaionToTarget, rotationStep);

            float distance = Vector3.Distance(transform.position, targetWaypoint.position);//distance do guarda ao  waypoint
            CheckDistanceWayPoint(distance);
            CheckPlayer();
        }
        //move o guarda
        //transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveStep);

    }

    public void CheckDistanceWayPoint(float currentDistance)
    {
        if (currentDistance <= minDist)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    public void UpdateTargetWaypoint()
    {
        //quando o guarda atingir o ultimo waypoint manda-o para o primiero de novo
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        targetWaypoint = waypoints[targetWaypointIndex];
    }

    public void CheckPlayer()//deteta o player
    {
        Vector3 posPlayer = player.position;
        posPlayer.y = transform.position.y; // evita que o boneco se incline quando chegar próximo ao jogador
        Vector3 dirPlayer = (posPlayer - transform.position).normalized;
        angulo = Vector3.Angle(transform.forward, dirPlayer);




        Ray ray = new Ray();
        ray.origin = transform.position + offset;
        ray.direction = transform.forward;

        Debug.DrawRay(transform.position + offset, transform.forward * range);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range) && isAlive)
        {
            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.GetComponent<Player>())
            {
                anim.Play("Descoberto");
                foundAnim.Play("DetetaIntruso");
                stopSound.Play();
                levelChanger.FadeToLevel();
              
            }
            if ( isAlive== false)
            {
                enemy.Die();
              //  Destroy(gameObject,1f)
            }

        }
    }


    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && p.isCrouched == false)
        {
            targetPos = col.transform.position;
            isRotating = true;
            Debug.Log("ENTROU");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isRotating = false;
    }

}
  