using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTGoToPosition : BTNode
{
    private string m_target;
    private bool m_movingTarget;

    public BTGoToPosition(string name, string target, bool movingTarget) : base(name)
    {
        m_target = target;
        m_movingTarget = movingTarget;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        Vector3 targPos;

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
                return controller.EndState(BTResult.Failure);
        }

        if (m_movingTarget)
        {
            NavMeshPath path = new NavMeshPath();
            if(!controller.agentSelf.CalculatePath(targPos, path))
            {
                return controller.EndState(BTResult.Success);
            }

            controller.agentSelf.SetPath(path);

        }
        else
        {
            if (!controller.agentSelf.hasPath)
            {
                controller.agentSelf.destination = targPos;
            }
        }

        if (controller.agentSelf.remainingDistance <= controller.agentSelf.stoppingDistance && 
                controller.agentSelf.pathStatus == NavMeshPathStatus.PathComplete)
        {
            return controller.EndState(BTResult.Success);
        }

        return controller.EndState(BTResult.Running);
    }
}
