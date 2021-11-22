using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQDotProduct : SQNode
{
    private string m_target;
    private bool m_invert;
    private bool m_abs;
    private float m_min;

    public SQDotProduct(SceneQuery parent, string target, bool invert, bool abs, float min) : base(parent)
    {
        m_invert = invert;
        m_target = target;
        m_abs = abs;
        m_min = min;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targDir;
        Vector3 targPos;

        if (m_target == null)
        {
            targPos = parent.transform.position;
            targDir = parent.transform.forward;
        }
        else
        {
            BlackBoardItem item;
            switch (parent.blackBoard.getItem(m_target, out item))
            {
                case BlackBoardItem.EType.Transform:
                    targPos = ((BBTransform)item).value.position;
                    targDir = ((BBTransform)item).value.forward;
                    break;
                case BlackBoardItem.EType.Agent:
                    targPos = ((BBAgent)item).value.transform.position;
                    targDir = ((BBAgent)item).value.transform.forward;
                    break;
                default:
                    return false;
            }
        }

        Queue<SceneQuery.QueryPoint> oldPoints = new Queue<SceneQuery.QueryPoint>();
        Queue<float> newWeights = new Queue<float>();

        foreach (SceneQuery.QueryPoint p in points)
        {
            Vector3 dir = (p.position - targPos).normalized;

            float dot = Vector3.Dot(dir, targDir);

            if (m_abs) dot = Mathf.Abs(dot);
            if(m_invert) dot = 1.0f - dot;

            dot = Mathf.Max(dot, m_min);

            if(dot > 0)
            {
                oldPoints.Enqueue(p);
                newWeights.Enqueue(dot);
            }

        }

        points.Clear();

        while (oldPoints.Count > 0)
        {
            SceneQuery.QueryPoint p = oldPoints.Dequeue();
            float weight = newWeights.Dequeue();

            p.weight *= weight;

            points.Add(p);
        }

        return true;
    }
}
