using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneQuery : MonoBehaviour
{
    public LayerMask groundMask;
    public AIBlackBoard blackBoard;

    public struct QueryPoint
    {
        public Vector3 position;
        public float weight;

        public QueryPoint(Vector3 position, float weight) { this.position = position; this.weight = weight; }
    }

    [Min(2)]
    public int numX = 2, numY = 2;
    [Min(0.001f)]
    public float width, height;

    [Min(0.0f)]
    public float upProjection, downProjection;
    [Min(0.0f)]
    public float groundOffset;

    [HideInInspector]
    public float maxWeight;

    public List<QueryPoint> queryPoints = new List<QueryPoint>();

    protected List<SQNode> nodes = new List<SQNode>();

    public bool Evaluate(out Vector3 winningPosition)
    {
        winningPosition = Vector3.zero;

        queryPoints.Clear();
        PositionPoints();

        foreach (SQNode n in nodes)
        {
            if (!n.PerformQuery(ref queryPoints)) return false;
        }

        winningPosition = Vector3.zero;
        maxWeight = 0.0f;

        foreach (QueryPoint p in queryPoints)
        {
            if (p.weight > maxWeight)
            {
                winningPosition = p.position;
                maxWeight = p.weight;
            }
        }

        return maxWeight != 0.0f;
    }

    private void PositionPoints()
    {
        float cx = width / 2.0f;
        float cy = height / 2.0f;

        float dx = 1.0f / (numX - 1) * width;
        float dy = 1.0f / (numY - 1) * height;

        for (int y = 0; y < numY; y++)
        {
            for (int x = 0; x < numX; x++)
            {
                Vector3 pointPosition = new Vector3(x * dx + transform.position.x - cx, 
                                                    transform.position.y, 
                                                    y * dy + transform.position.z - cy);

                // up ray cast
                RaycastHit hit;
                if (Physics.Raycast(new Ray(pointPosition + Vector3.up * upProjection, Vector3.down), out hit, upProjection, groundMask))
                {
                    pointPosition.y = hit.point.y + groundOffset;
                }
                else if (Physics.Raycast(new Ray(pointPosition, Vector3.down), out hit, downProjection, groundMask))
                {
                    pointPosition.y = hit.point.y + groundOffset;
                }

                queryPoints.Add(new QueryPoint(pointPosition, 1.0f));
            }
        }
    }
}
