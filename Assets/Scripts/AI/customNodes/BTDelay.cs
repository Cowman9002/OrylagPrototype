using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDelay : BTNode
{
    private float m_endTime;
    private float m_delay;

    public BTDelay(float time)
    {
        m_delay = time;
        m_endTime = -1;
    }

    public override BTResult Evaluate()
    {
        if(m_endTime < 0)
        {
            m_endTime = Time.time + m_delay;
            controller.SetEvaluatingNode(this);
        }
        else
        {
            if(Time.time >= m_endTime)
            {
                m_endTime = -1;
                controller.FinishState();
                return BTResult.Success;
            }
        }

        return BTResult.Running;
    }
}
