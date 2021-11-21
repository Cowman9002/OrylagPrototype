using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTHasBBItem : BTNode
{
    private string m_itemName;
    public BTHasBBItem(string itemName)
    {
        m_itemName = itemName;
    }

    public override BTResult Evaluate()
    {
        Object obj;
        if (!controller.getItemFromBB(m_itemName, out obj)) return BTResult.Failure;
        return BTResult.Success;
    }
}
