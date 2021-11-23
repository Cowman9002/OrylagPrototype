using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSetLookTarget : BTNode
{
    private string m_target;
    public BTSetLookTarget(string name, string target) : base(name)
    {
        m_target = target;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        if(m_target == null)
        {
            controller.SetLookTarget(null);
        }
        else
        {
            BlackBoardItem item;
            switch (controller.blackBoard.getItem(m_target, out item))
            {
                case BlackBoardItem.EType.Transform:
                    controller.SetLookTarget(((BBTransform)item).value);
                    break;
                default:
                    return controller.EndState(BTResult.Failure);
            }
        }

        return controller.EndState(BTResult.Success);
    }
}
