﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach(var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    break;
            }

            if (_nodeState == NodeState.SUCCESS)
            {
                break;
            }
        }
        return _nodeState;
    }
}


