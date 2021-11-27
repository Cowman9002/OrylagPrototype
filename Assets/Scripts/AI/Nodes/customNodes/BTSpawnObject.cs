using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSpawnObject : BTNode
{
    private string m_origin;
    private GameObject m_obj;

    public BTSpawnObject(string name, string origin, GameObject obj) : base(name)
    {
        m_origin = origin;
        m_obj = obj;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        Transform origin;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem(m_origin, out item))
        {
            case BlackBoardItem.EType.Transform:
                origin = ((BBTransform)item).value;
                break;
            default:
                return controller.EndState(BTResult.Failure);
        }

        GameObject.Instantiate(m_obj, origin.position, origin.rotation);

        return controller.EndState(BTResult.Success);
    }
}
