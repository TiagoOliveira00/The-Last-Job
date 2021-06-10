using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtNode : Node
{
    private Transform target;
    private Transform origin;

    private float yRotation = -90f;

    public LookAtNode(Transform target, Transform origin)
    {
        this.target = target; // posição do player
        this.origin = origin; // posição da cam
    }

    public override NodeState Evaluate()
    {
        Vector3 lookAt = target.position - origin.position;
        Transform teste = origin;
        teste.forward = lookAt;

        if (teste.rotation.y >= -75 && teste.rotation.y <= -175)
        {
            
        }
        else
        {
            origin.forward = lookAt;
        }

        return NodeState.SUCCESS;
    }
}
