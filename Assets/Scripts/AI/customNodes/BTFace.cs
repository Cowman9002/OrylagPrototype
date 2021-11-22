using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTFace : BTNode
{
    private AIBlackBoard.BlackBoardElement m_target;
    private float m_speed;

    public BTFace(AIBlackBoard.BlackBoardElement target, float speed)
    {
        m_target = target;
        m_speed = speed;
    }

    public override BTResult Evaluate()
    {
        Vector3 targPos;

        switch (m_target.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                Transform transform;
                if (!controller.blackBoard.getItem(m_target.key, out transform))
                    return BTResult.Failure;
                targPos = transform.position;
                break;
            default: return BTResult.Failure;
        }

        Vector3 targDir = (targPos - controller.transform.position);
        targDir.y = 0.0f;
        targDir.Normalize();

        Quaternion targRot = Quaternion.LookRotation(targDir);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targRot, Time.fixedDeltaTime * m_speed);

        if(Quaternion.Angle(controller.transform.rotation, targRot) < 0.5)
        {
            controller.transform.rotation = targRot;
            return BTResult.Success;
        }

        return BTResult.Running;
    }
}
