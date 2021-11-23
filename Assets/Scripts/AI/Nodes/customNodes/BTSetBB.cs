using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBB : BTNode
{
    private string m_name;
    BlackBoardItem m_value;

    public BTSetBB(string name, string key, BlackBoardItem value) : base(name)
    {
        m_name = key;
        m_value = value;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        controller.blackBoard.setItem(m_name, m_value);

        return controller.EndState(BTResult.Success);
    }
}
