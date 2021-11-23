using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTDelay : BTNode
{
    private float m_endTime;
    private float m_delay;
    private bool m_isActive = false;

    private string m_abortName;
    bool m_abortState;

    public BTDelay(string name, float time, string abortName, bool abortState) : base(name)
    {
        m_delay = time;
        m_endTime = -1;

        m_abortName = abortName;
        m_abortState = abortState;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        if (!m_isActive)
        {
            m_isActive = true;
            m_endTime = Time.time + m_delay;
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
                        return controller.EndState(BTResult.Failure);
                }

                if(v == m_abortState)
                {
                    m_isActive = false;
                    return controller.EndState(BTResult.Failure);
                }
            }

            //Debug.Log("Waiting: " + (m_endTime - Time.time));

            if(Time.time >= m_endTime)
            {
                m_isActive = false;
                return controller.EndState(BTResult.Success);
            }
        }

        return controller.EndState(BTResult.Running);
    }
}
