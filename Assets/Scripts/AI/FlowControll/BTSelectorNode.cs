using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectorNode : BTFlowNode
{
    public BTSelectorNode(List<BTNode> children) : base(children) { }

    public override BTResult Evaluate()
    {
        foreach (BTNode node in this.children)
        {
            switch(node.Evaluate())
            {
                case BTResult.Running:
                    return BTResult.Running;

                case BTResult.Success:
                    return BTResult.Success;

                case BTResult.Failure:
                    continue;
            }
        }

        return BTResult.Failure;
    }
}
