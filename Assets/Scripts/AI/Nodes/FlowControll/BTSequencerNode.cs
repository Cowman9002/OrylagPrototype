using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequencerNode : BTFlowNode
{
    int m_currentChildIndex = 0;
    private bool m_lastChildFailed = false;

    public BTSequencerNode(string name, List<BTNode> children) : base(name, children) { }

    public override BTController.BTStateEndData Evaluate()
    {
        if (children.Count == 0) return controller.EndState(BTResult.Success);

        if(m_lastChildFailed)
        {
            m_lastChildFailed = false;
            m_currentChildIndex = 0;
            return controller.EndState(BTResult.Failure);
        }

        m_lastChildFailed = false;

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
                    break;

                case BTResult.Failure:
                    m_currentChildIndex = 0;
                    return controller.EndState(BTResult.Failure);
            }
        }

        m_currentChildIndex = 0;
        return controller.EndState(BTResult.Success);
    }

    public override void ChildEnded(BTController.BTStateEndData data)
    {
        switch (data.result)
        {
            case BTResult.Failure:
                m_lastChildFailed = true; break;
        }
    }
}
