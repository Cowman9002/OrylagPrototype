using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensor : AgentSensor
{
    public LayerMask layerMask;

    [Range(0.001f, 180.0f)]
    public float halfViewAngle = 20.0f;
    [Min(0.001f)]
    public float viewRadius = 10.0f;

    private HashSet<Collider> colliderSet = new HashSet<Collider>();
    private HashSet<Collider> currentColliderSet = new HashSet<Collider>();
    private Dictionary<Transform, uint> visibleObjects = new Dictionary<Transform, uint>();

    public override bool hasTransform(Transform t)
    {
        return visibleObjects.ContainsKey(t);
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius, layerMask);

        foreach(Collider col in colliders)
        {
            if (col.GetType() == typeof(MeshCollider) && !((MeshCollider)col).convex) continue;

            bool visible = DoVisionTest(col);

            if (visible)
            {
                currentColliderSet.Add(col);

                if (!colliderSet.Contains(col)) // new collider
                {
                    colliderSet.Add(col);

                    uint count;
                    if (visibleObjects.TryGetValue(col.transform, out count))
                    {
                        visibleObjects[col.transform] = count + 1;

                    }
                    else
                    {
                        visibleObjects.Add(col.transform, 0);
                        controller.SensorObjectEnter(col.transform);
                    }
                }
            }
        }

        Collider[] colliderCopy = new Collider[colliderSet.Count];
        colliderSet.CopyTo(colliderCopy);

        foreach (Collider c in colliderCopy)
        {
            if (!currentColliderSet.Contains(c) && c != null) // collider no longer visible
            {
                colliderSet.Remove(c);
                uint count;
                if (visibleObjects.TryGetValue(c.transform, out count))
                {
                    if (count == 0)
                    {
                        visibleObjects.Remove(c.transform);
                        controller.SensorObjectExit(c.transform);
                    }
                    else
                    {
                        visibleObjects[c.transform] = count - 1;
                    }
                }
            }
        }

        currentColliderSet.Clear();
    }

    private bool DoVisionTest(Collider other)
    {
        bool visible = false;
        Vector3 toTarget = other.ClosestPoint(transform.position) - transform.position;

        toTarget.Normalize();
        if (InCone(toTarget))
        {
            RaycastHit hit;
            if(Physics.SphereCast(new Ray(transform.position, toTarget), 0.2f, out hit, viewRadius, layerMask))
            {
                if (hit.collider == other)
                {
                    visible = true;
                }
            }
        }

        //Debug.DrawRay(transform.position, toTarget * viewRadius, visible ? Color.green : Color.red);

        return visible;
    }

    private bool InCone(Vector3 toVector)
    {
        bool res = Mathf.Abs(Vector3.Angle(toVector, transform.forward)) <= halfViewAngle;
        return res;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 startDir = Quaternion.AngleAxis(halfViewAngle, Vector3.up) * transform.forward;
        Vector3 endDir = Quaternion.AngleAxis(halfViewAngle, -Vector3.up) * transform.forward;

        Gizmos.DrawRay(transform.position, startDir * viewRadius);
        Gizmos.DrawRay(transform.position, endDir * viewRadius);

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(transform.position, transform.forward * viewRadius);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, viewRadius);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.up, endDir, halfViewAngle * 2, viewRadius * 0.2f);
    }
}
