using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQRandomNode : SQNode
{
    private float m_min, m_max;
    public SQRandomNode(SceneQuery parent, float min, float max) : base(parent)
    {
        m_min = Mathf.Clamp01(min);
        m_max = Mathf.Clamp01(max);
    }

    public override bool PerformQuery(ref List<SceneQuery.QueryPoint> points)
    {
        Queue<SceneQuery.QueryPoint> oldPoints = new Queue<SceneQuery.QueryPoint>();

        foreach (SceneQuery.QueryPoint p in points)
        {
            float value = Random.Range(m_min, m_max);
            oldPoints.Enqueue(new SceneQuery.QueryPoint(p.position, value));
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
