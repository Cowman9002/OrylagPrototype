using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFlowNode : BTNode
{
    protected List<BTNode> children;

    public BTFlowNode(List<BTNode> children)
    {
        this.children = children;
        foreach(BTNode node in this.children)
        {
            node.parent = this;
        }
    }

    public override void setController(BTController controller)
    {
        base.setController(controller);
        foreach (BTNode node in children)
        {
            node.setController(controller);
        }
    }
}
