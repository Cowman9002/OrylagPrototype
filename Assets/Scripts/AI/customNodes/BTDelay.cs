using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTDelay : BTNode
{
    private float m_endTime;
    private float m_delay;

    private string m_abortName;
    bool m_abortState;

    public BTDelay(float time, string abortName, bool abortState)
    {
        m_delay = time;
        m_endTime = -1;

        m_abortName = abortName;
        m_abortState = abortState;
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
            if(m_abortName != null)
            {
                bool v;
                BlackBoardItem item;
                switch (controller.blackBoard.getItem(m_abortName, out item))
                {
                    case BlackBoardItem.EType.Bool:
                        v = ((BBBool)item).value;
                        break;
                    case BlackBoardItem.EType.Transform:
                        v = ((BBTransform)item).value != null;
                        break;
                    case BlackBoardItem.EType.Agent:
                        v = ((BBAgent)item).value != null;
                        break;
                    case BlackBoardItem.EType.Vector:
                        v = ((BBVector)item).value != null;
                        break;
                    default:
                        return BTResult.Failure;
                }

                if(v == m_abortState)
                {
                    m_endTime = -1;
                    controller.FinishState();
                    return BTResult.Failure;
                }
            }

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
