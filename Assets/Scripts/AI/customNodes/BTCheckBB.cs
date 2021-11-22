using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckBB : BTNode
{
    private string m_item;

    public BTCheckBB(string item)
    {
        m_item = item;
    }

    public override BTResult Evaluate()
    {
        bool v;

        BlackBoardItem item;
        switch (controller.blackBoard.getItem(m_item, out item))
        {
            case BlackBoardItem.EType.Bool:
                v = ((BBBool)item).value;
                break;
            case BlackBoardItem.EType.Transform:
                v = ((BBTransform)item).value != null;
                break;
            case BlackBoardItem.EType.Agent:
                v = ((BBAgent)item).value != null;
                break;
            case BlackBoardItem.EType.Vector:
                v = ((BBVector)item).value != null;
                break;
            default:
                return BTResult.Failure;
        }

        return v ? BTResult.Success : BTResult.Failure;
    }
}
