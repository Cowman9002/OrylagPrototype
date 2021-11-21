using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequencerNode : BTFlowNode
{
    public BTSequencerNode(List<BTNode> children) : base(children) { }

    public override BTResult Evaluate()
    {
        foreach (BTNode node in this.children)
        {
            switch (node.Evaluate())
            {
                case BTResult.Running:
                    return BTResult.Running;

                case BTResult.Success:
                    continue;

                case BTResult.Failure:
                    return BTResult.Failure;
            }
        }

        return BTResult.Success;
    }
}
