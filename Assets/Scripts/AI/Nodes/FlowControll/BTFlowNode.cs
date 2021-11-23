using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFlowNode : BTNode
{
    protected List<BTNode> children;

    public BTFlowNode(string name, List<BTNode> children) : base(name)
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
            node.parent = this;
            node.ValidateName();
            node.setController(controller);
        }
    }
}
