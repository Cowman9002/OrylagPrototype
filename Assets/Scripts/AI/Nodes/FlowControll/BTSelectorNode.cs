using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectorNode : BTFlowNode
{
    int m_currentChildIndex = 0;
    private bool m_lastChildSucceeded = false;
    public BTSelectorNode(string name, List<BTNode> children) : base(name, children) { }

    public override BTController.BTStateEndData Evaluate()
    {
        if (children.Count == 0) return controller.EndState(BTResult.Success);

        if (m_lastChildSucceeded)
        {
            m_lastChildSucceeded = false;
            m_currentChildIndex = 0;
            return controller.EndState(BTResult.Success);
        }

        m_lastChildSucceeded = false;

        while (m_currentChildIndex < children.Count)
        {
            BTNode node = children[m_currentChildIndex];
            controller.BeginState(node);

            m_currentChildIndex++;

            switch (node.Evaluate().result)
            {
                case BTResult.Running:
                    return controller.EndState(BTResult.Running);

                case BTResult.Success:
                    m_currentChildIndex = 0;
                    return controller.EndState(BTResult.Success);

                case BTResult.Failure:
                    break;
            }
        }


        m_currentChildIndex = 0;
        return controller.EndState(BTResult.Failure);
    }

    public override void ChildEnded(BTController.BTStateEndData data)
    {
        switch (data.result)
        {
            case BTResult.Success:
                m_lastChildSucceeded = true; break;
        }
    }
}
