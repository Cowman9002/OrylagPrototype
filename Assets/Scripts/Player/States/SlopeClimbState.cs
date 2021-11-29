using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeClimbState : PlayerLookState
{
    private int m_groundMask;

    public SlopeClimbState(PlayerMovementController controller) : base(controller)
    {
        m_groundMask = 1 << LayerMask.NameToLayer("Level");
    }

    protected Vector3 CaclSlopeDirection(Vector3 normal)
    {
        Vector3 slope_dir = controller.Velocity.normalized + Vector3.up * -Vector3.Dot(normal, controller.Velocity.normalized);
        slope_dir.Normalize();
        Debug.DrawRay(controller.transform.position, slope_dir * 4.0f, Color.red);
        return slope_dir * controller.Velocity.magnitude;
    }

    protected void SnapToGround(float height)
    {
        Vector3 newPos = controller.transform.position;
        newPos.y = height;
        controller.transform.position = newPos;
    }

    protected bool GetGroundNormal(out Vector3 normal, out float height)
    {
        Ray ray = new Ray(controller.transform.position + Vector3.up * 0.5f, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, 0.8f, m_groundMask))
        {
            normal = hit.normal;
            height = hit.point.y;
            return true;
        }

        normal = Vector3.zero;
        height = 0;
        return false;
    }
}
