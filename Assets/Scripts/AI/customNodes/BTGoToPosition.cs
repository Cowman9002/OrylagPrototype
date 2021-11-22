using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTGoToPosition : BTNode
{
    private AIBlackBoard.BlackBoardElement m_target;
    private bool m_movingTarget;
    public BTGoToPosition(AIBlackBoard.BlackBoardElement target, bool movingTarget)
    {
        m_target = target;
        m_movingTarget = movingTarget;
    }

    public override BTResult Evaluate()
    {
        Vector3 targPos;
        NavMeshAgent agent;

        switch(m_target.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:

                Transform target;
                if (!controller.blackBoard.getItem(m_target.key, out target)) return BTResult.Failure;
                targPos = target.position;

                break;
            default: return BTResult.Failure;
        }

        
        if (!controller.blackBoard.getItem("SelfAgent", out agent)) return BTResult.Failure;

        if (m_movingTarget)
        {
            agent.destination = targPos;
        }
        else
        {
            if (!agent.hasPath)
            {
                agent.destination = targPos;
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return BTResult.Success;
        }

        return BTResult.Running;
    }
}
