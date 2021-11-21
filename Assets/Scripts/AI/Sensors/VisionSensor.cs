using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class VisionSensor : AgentSensor
{
    public LayerMask layerMask;

    [Range(0.001f, 90.0f)]
    public float viewAngle;
    [Min(0.001f)]
    public float viewDistance;

    private HashSet<Collider> colliders = new HashSet<Collider>();
    private Dictionary<Transform, uint> oldObjects = new Dictionary<Transform, uint>();

    private CapsuleCollider m_collider;

    private void Start()
    {
        m_collider = GetComponent<CapsuleCollider>();
        m_collider.isTrigger = true;
        UpdateViewAngle();
    }

    public override bool hasTransform(Transform t)
    {
        return oldObjects.ContainsKey(t);
    }

    public void UpdateViewAngle()
    {
        m_collider.height = viewDistance * Mathf.PI;
        m_collider.center = new Vector3(0.0f, 0.0f, m_collider.height * 0.5f);
        m_collider.radius = Mathf.Sin(Mathf.Deg2Rad * viewAngle) * viewDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        IsVisible(other);
    }

    private void OnTriggerStay(Collider other)
    {
        bool visible = IsVisible(other);

        if (colliders.Contains(other))
        {
            uint count;
            if (!visible && oldObjects.TryGetValue(other.transform, out count))
            {
                colliders.Remove(other);
                if (count == 0)
                {
                    oldObjects.Remove(other.transform);
                    controller.SensorObjectExit(other.transform);
                }
                else
                {
                    oldObjects[other.transform] = count - 1;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other))
        {
            uint count;
            if (oldObjects.TryGetValue(other.transform, out count))
            {
                colliders.Remove(other);
                if (count == 0)
                {
                    oldObjects.Remove(other.transform);
                    controller.SensorObjectExit(other.transform);
                }
                else
                {
                    oldObjects[other.transform] = count - 1;
                }
            }
        }
    }

    private bool IsVisible(Collider other)
    {
        bool visible = false;
        Vector3 toTarget = other.ClosestPoint(transform.position) - transform.position;

        if (toTarget.magnitude <= viewDistance)
        {
            toTarget.Normalize();
            if (InCone(toTarget))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toTarget, out hit, viewDistance, layerMask))
                {
                    if (hit.collider == other)
                    {
                        visible = true;

                        if(!colliders.Contains(other))
                        {
                            colliders.Add(other);

                            uint count;
                            if (oldObjects.TryGetValue(other.transform, out count))
                            {
                                oldObjects[other.transform] = count + 1;

                            }
                            else
                            {
                                oldObjects.Add(other.transform, 0);
                                controller.SensorObjectEnter(other.transform);
                            }
                        }
                    }
                }
            }
        }
        Debug.DrawRay(transform.position, toTarget * viewDistance, visible ? Color.green : Color.red);

        return visible;
    }

    private bool InCone(Vector3 toVector)
    {
        bool res = Mathf.Abs(Vector3.Angle(toVector, transform.forward)) <= viewAngle;

        Color c = res ? Color.green : Color.red;
        Debug.DrawRay(transform.position, toVector, c);

        return res;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(viewAngle, transform.right) * transform.forward * viewDistance);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(viewAngle, -transform.right) * transform.forward * viewDistance);

        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward * viewDistance);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(viewAngle, -transform.up) * transform.forward * viewDistance);

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
