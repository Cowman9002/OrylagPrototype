using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQVisibleNode : SQNode
{
    private bool m_invert;
    private float m_radius;
    private string m_target;

    public SQVisibleNode(SceneQuery parent, string target, float radius, bool invert) : base(parent)
    {
        m_invert = invert;
        m_target = target;
        m_radius = radius;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targPos;

        if (m_target == null)
        {
            targPos = parent.transform.position;
        }
        else
        {
            BlackBoardItem item;
            switch (parent.blackBoard.getItem(m_target, out item))
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
                    return false;
            }
        }

        Queue<SceneQuery.QueryPoint> oldPoints = new Queue<SceneQuery.QueryPoint>();

        foreach (SceneQuery.QueryPoint p in points)
        {
            Vector3 dir = (targPos - p.position).normalized;
            RaycastHit hit;
            bool visible = false;

            if(Physics.Raycast(new Ray(p.position, dir), out hit))
            {
                if((targPos - hit.point).sqrMagnitude <= m_radius * m_radius)
                {
                    visible = true;
                    //Debug.DrawLine(p.position, hit.point);
                }
            }

            if(!m_invert && visible || m_invert && !visible)
            {
                oldPoints.Enqueue(p);
            }
        }

        points.Clear();

        while (oldPoints.Count > 0)
        {
            points.Add(oldPoints.Dequeue());
        }

        return true;
    }
}
