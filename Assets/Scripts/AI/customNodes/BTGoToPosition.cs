using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTGoToPosition : BTNode
{
    private string m_target;
    private bool m_movingTarget;

    public BTGoToPosition(string target, bool movingTarget)
    {
        m_target = target;
        m_movingTarget = movingTarget;
    }

    public override BTResult Evaluate()
    {
        Vector3 targPos;
        NavMeshAgent agent;

        BlackBoardItem item;
        switch(controller.blackBoard.getItem(m_target, out item))
        {
            case BlackBoardItem.EType.Transform:
                targPos = ((BBTransform)item).value.position;
                break;
            case BlackBoardItem.EType.Agent:
                targPos = ((BBAgent)item).value.transform.position;
                break;
            case BlackBoardItem.EType.Vector:
                targPos = ((BBVector)item).value;
                break;
            default:
                controller.FinishState();
                return BTResult.Failure;
        }

        switch (controller.blackBoard.getItem("SelfAgent", out item))
        {
            case BlackBoardItem.EType.Agent:
                agent = ((BBAgent)item).value;
                break;
            default:
                controller.FinishState();
                return BTResult.Failure;
        }

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
            controller.FinishState();
            return BTResult.Success;
        }

        controller.SetEvaluatingNode(this);
        return BTResult.Running;
    }
}
