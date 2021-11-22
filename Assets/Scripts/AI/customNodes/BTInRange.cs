using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInRange : BTNode
{
    private string m_target;
    private float m_sqrRange;
    public BTInRange(string target, float range)
    {
        m_target = target;
        m_sqrRange = range * range;
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
                return BTResult.Failure;
        }

        float dist = Vector3.SqrMagnitude(targPos - controller.transform.position);

        if (dist < m_sqrRange)
        {
            return BTResult.Success;
        }
        else
        {
            return BTResult.Failure;
        }

    }
}
