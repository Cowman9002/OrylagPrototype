using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public enum BTResult
    {
        Running, Success, Failure
    }

    public BTNode parent;
    protected List<BTNode> children;

    public BTNode(List<BTNode> children)
    {

    }

    public virtual BTResult Evaluate() { return BTResult.Failure; }
}
