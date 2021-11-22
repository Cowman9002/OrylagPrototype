using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQDistanceNode : SQNode
{
    private bool m_invert;
    private string m_target;

    public SQDistanceNode(SceneQuery parent, string target, bool invert) : base(parent)
    {
        m_invert = invert;
        m_target = target;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targPos;

        if(m_target == null)
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
        Queue<float> newWeights = new Queue<float>();
        float maxDistance = -1.0f;

        foreach (SceneQuery.QueryPoint p in points)
        {
            float dist = Vector3.Distance(p.position, targPos);
            if (dist > maxDistance) maxDistance = dist;

            oldPoints.Enqueue(p);
            newWeights.Enqueue(dist);
        }

        points.Clear();

        while (oldPoints.Count > 0)
        {
            SceneQuery.QueryPoint p = oldPoints.Dequeue();

            if(m_invert)
            {
                p.weight *= 1.0f - newWeights.Dequeue() / maxDistance;
            }
            else
            {
                p.weight *= newWeights.Dequeue() / maxDistance;
            }


            points.Add(p);
        }

        return true;
    }
}
