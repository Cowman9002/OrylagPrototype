using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : BTNode
{
    protected BTFlowNode child;
    public BTRoot(BTFlowNode child)
    {
        this.child = child;
        this.child.controller = controller;
    }

    public override BTResult Evaluate()
    {
        return child.Evaluate();
    }

    public override void setController(BTController controller)
    {
        base.setController(controller);
        child.setController(controller);
    }
}
