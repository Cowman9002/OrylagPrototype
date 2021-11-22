using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBB : BTNode
{
    private string m_name;
    BlackBoardItem m_value;

    public BTSetBB(string name, BlackBoardItem value)
    {
        m_name = name;
        m_value = value;
    }

    public override BTResult Evaluate()
    {
        controller.blackBoard.setItem(m_name, m_value);

        return BTResult.Success;
    }
}
