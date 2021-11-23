using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRandomLocation : BTNode
{
    private float m_radius;
    private string m_center;
    private string m_storage;

    public BTRandomLocation(string name, string center, float radius, string storageName) : base(name)
    {
        m_radius = radius;
        m_storage = storageName;
        m_center = center;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        Vector3 centerPos;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem(m_center, out item))
        {
            case BlackBoardItem.EType.Transform:
                centerPos = ((BBTransform)item).value.position;
                break;
            case BlackBoardItem.EType.Agent:
                centerPos = ((BBAgent)item).value.transform.position;
                break;
            case BlackBoardItem.EType.Vector:
                centerPos = ((BBVector)item).value;
                break;
            default:
                return controller.EndState(BTResult.Failure);
        }

        Vector3 position = new Vector3(0.0f, centerPos.y + 1.5f, 0.0f);

        do
        {
            position.x = centerPos.x + Random.Range(-m_radius, m_radius);
            position.z = centerPos.z + Random.Range(-m_radius, m_radius);
        }
        while (Physics.OverlapSphere(position, 0.6f).Length != 0);

        controller.blackBoard.setItem(m_storage, new BBVector(position));

        return controller.EndState(BTResult.Success);
    }
}
