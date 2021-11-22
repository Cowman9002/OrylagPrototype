using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAgentStats : BTNode
{
    private bool m_allowRotation;
    public BTAgentStats(bool allowRotation)
    {
        m_allowRotation = allowRotation;
    }

    public override BTResult Evaluate()
    {
        NavMeshAgent agent;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem("SelfAgent", out item))
        {
            case BlackBoardItem.EType.Agent:
                agent = ((BBAgent)item).value;
                break;
            default:
                return BTResult.Failure;
        }

        agent.updateRotation = m_allowRotation;

        return BTResult.Success;
    }
}
