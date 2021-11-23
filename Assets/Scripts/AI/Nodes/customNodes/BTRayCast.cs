using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTRayCast : BTNode
{
    private string m_target;
    private LayerMask m_mask;
    private Vector3 m_offset;
    private float m_distance;
    private float m_radius;

    public BTRayCast(string name, string target, LayerMask mask, Vector3 offset, float distance, float radius) : base(name)
    {
        m_target = target;
        m_mask = mask;
        m_offset = offset;
        m_distance = distance;
        m_radius = radius;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        Transform targTransform;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem(m_target, out item))
        {
            case BlackBoardItem.EType.Transform:
                targTransform = ((BBTransform)item).value;
                break;
            case BlackBoardItem.EType.Agent:
                targTransform = ((BBAgent)item).value.transform;
                break;
            default:
                return controller.EndState(BTResult.Failure);
        }

        Ray ray = new Ray(controller.transform.position + m_offset, (targTransform.position - controller.transform.position).normalized);


        RaycastHit hit;

        if(m_radius <= 0.0f)
        {
            if (Physics.Raycast(ray, out hit, m_distance, m_mask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.transform == targTransform)
                {
                    return controller.EndState(BTResult.Success);
                }
            }
        }
        else
        {
            if (Physics.SphereCast(ray, m_radius, out hit, m_distance, m_mask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.transform == targTransform)
                {
                    return controller.EndState(BTResult.Success);
                }
            }
        }

        return controller.EndState(BTResult.Failure);
    }
}
