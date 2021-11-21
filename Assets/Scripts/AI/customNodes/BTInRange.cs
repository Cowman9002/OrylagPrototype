using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInRange : BTNode
{
    private string m_targetName;
    private float m_sqrRange;
    public BTInRange(string targetName, float range)
    {
        m_targetName = targetName;
        m_sqrRange = range * range;
    }

    public override BTResult Evaluate()
    {
        Transform target;
        if (!controller.getItemFromBB(m_targetName, out target)) return BTResult.Failure;

        float dist = Vector3.SqrMagnitude(target.position - controller.transform.position);
        Debug.Log(dist);

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
