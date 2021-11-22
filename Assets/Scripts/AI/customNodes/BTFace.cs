using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTFace : BTNode
{
    private string m_target;
    private float m_speed;

    public BTFace(string target, float speed)
    {
        m_target = target;
        m_speed = speed;
    }

    public override BTResult Evaluate()
    {
        Vector3 targPos;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem(m_target, out item))
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

        Vector3 targDir = (targPos - controller.transform.position);
        targDir.y = 0.0f;
        targDir.Normalize();

        Quaternion targRot = Quaternion.LookRotation(targDir);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targRot, Time.fixedDeltaTime * m_speed);

        if(Quaternion.Angle(controller.transform.rotation, targRot) < 0.5)
        {
            controller.transform.rotation = targRot;
            controller.FinishState();
            return BTResult.Success;
        }

        controller.SetEvaluatingNode(this);
        return BTResult.Running;
    }
}
