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
        if (!controller.blackBoard.getItem("SelfAgent", out agent)) return BTResult.Failure;

        agent.updateRotation = m_allowRotation;

        return BTResult.Success;
    }
}
