using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTGoToPosition : BTNode
{
    private string m_targetName;
    private bool m_movingTarget;
    public BTGoToPosition(string targetName, bool movingTarget)
    {
        m_targetName = targetName;
        m_movingTarget = movingTarget;
    }

    public override BTResult Evaluate()
    {
        Transform target;
        NavMeshAgent agent;

        if (!controller.getItemFromBB(m_targetName, out target)) return BTResult.Failure;
        if (!controller.getItemFromBB("SelfAgent", out agent)) return BTResult.Failure;

        if (m_movingTarget)
        {
            agent.destination = target.position;
            return BTResult.Success;
        }
        else
        {
            if (!agent.hasPath)
            {
                agent.destination = target.position;
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                return BTResult.Success;
            }

            return BTResult.Running;    
        }
    }
}
