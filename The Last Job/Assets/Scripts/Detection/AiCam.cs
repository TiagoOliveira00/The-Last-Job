using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCam : MonoBehaviour
{
    Player p;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float range;

    private void Start()
    {
        p = FindObjectOfType<Player>();
        playerTransform = p.transform;
    }

    private void ConstructBehaviourTree()
    {
        RangeNode rangeNode = new RangeNode(range, playerTransform, transform);
        LookAtNode lookAtNode = new LookAtNode(playerTransform, transform);

        Sequence lookAtSequence = new Sequence(new List<Node>() { rangeNode, lookAtNode });
        lookAtSequence.Evaluate();
    }

    public void Update()
    {
        ConstructBehaviourTree();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("morreu");
        }
    }
}
