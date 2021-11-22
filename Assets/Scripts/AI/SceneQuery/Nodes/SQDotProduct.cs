using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQDotProduct : SQNode
{
    private AIBlackBoard.BlackBoardElement m_target;
    private bool m_invert;

    public SQDotProduct(SceneQuery parent, AIBlackBoard.BlackBoardElement target, bool invert) : base(parent)
    {
        m_invert = invert;
        m_target = target;
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Vector3 targDir;
        Vector3 targPos;

        switch (m_target.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                Transform transform;
                if (!parent.blackBoard.getItem(m_target.key, out transform)) return false;
                targDir = transform.forward;
                targPos = transform.position;

                break;
            default: return false;
        }

        Queue<SceneQuery.QueryPoint> oldPoints = new Queue<SceneQuery.QueryPoint>();
        Queue<float> newWeights = new Queue<float>();

        foreach (SceneQuery.QueryPoint p in points)
        {
            Vector3 dir = (p.position - targPos).normalized;

            float dot = Vector3.Dot(dir, targDir);

            if (!m_invert && dot >= 0.0f || m_invert && dot <= 0.0f)
            {
                oldPoints.Enqueue(p);
                newWeights.Enqueue(Mathf.Abs(dot));
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
