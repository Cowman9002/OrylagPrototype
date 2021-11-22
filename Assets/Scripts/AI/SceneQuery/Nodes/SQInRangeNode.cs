using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQInRangeNode : SQNode
{
    private AIBlackBoard.BlackBoardElement m_target;
    private bool m_invert;
    private float m_sqrDist;

    public SQInRangeNode(SceneQuery parent, AIBlackBoard.BlackBoardElement target, float distance, bool invert) : base(parent)
    {
        m_invert = invert;
        m_target = target;
        m_sqrDist = distance * distance;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targPos;

        switch (m_target.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                Transform transform;
                if (!parent.blackBoard.getItem(m_target.key, out transform)) return false;
                targPos = transform.position;

                break;
            default: return false;
        }

        Queue<SceneQuery.QueryPoint> oldPoints = new Queue<SceneQuery.QueryPoint>();

        foreach (SceneQuery.QueryPoint p in points)
        {
            float sqrdst = (targPos - p.position).sqrMagnitude;

            if (!m_invert && sqrdst <= m_sqrDist || m_invert && sqrdst >= m_sqrDist)
            {
                oldPoints.Enqueue(p);
            }
        }

        points.Clear();

        while (oldPoints.Count > 0)
        {
            SceneQuery.QueryPoint p = oldPoints.Dequeue();
            points.Add(p);
        }

        return true;
    }
}
