using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInRange : BTNode
{
    private AIBlackBoard.BlackBoardElement m_target;
    private float m_sqrRange;
    public BTInRange(AIBlackBoard.BlackBoardElement target, float range)
    {
        m_target = target;
        m_sqrRange = range * range;
    }

    public override BTResult Evaluate()
    {
        Vector3 targPos;

        switch (m_target.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                Transform transform;
                if (!controller.blackBoard.getItem(m_target.key, out transform)) return BTResult.Failure;
                targPos = transform.position;

                break;
            default: return BTResult.Failure;
        }

        float dist = Vector3.SqrMagnitude(targPos - controller.transform.position);
        //Debug.Log(Mathf.Sqrt(dist));

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
