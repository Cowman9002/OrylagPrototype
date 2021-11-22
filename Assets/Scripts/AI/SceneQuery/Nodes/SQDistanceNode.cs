using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQDistanceNode : SQNode
{
    private bool m_invert;
    private AIBlackBoard.BlackBoardElement m_target;

    public SQDistanceNode(SceneQuery parent, AIBlackBoard.BlackBoardElement target, bool invert) : base(parent)
    {
        m_invert = invert;
        m_target = target;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targPos;

        if(m_target.key == null)
        {
            targPos = parent.transform.position;
        }
        else
        {
            switch (m_target.type)
            {
                case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                    Transform transform;
                    if (!parent.blackBoard.getItem(m_target.key, out transform)) return false;
                    targPos = transform.position;

                    break;
                default: return false;
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
