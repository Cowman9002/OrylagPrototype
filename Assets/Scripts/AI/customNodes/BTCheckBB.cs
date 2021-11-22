using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckBB : BTNode
{
    private AIBlackBoard.BlackBoardElement m_item;
    public BTCheckBB(AIBlackBoard.BlackBoardElement item)
    {
        m_item = item;
    }

    public override BTResult Evaluate()
    {
        switch(m_item.type)
        {
            case AIBlackBoard.BlackBoardElement.ElementType.Transform:
                Transform t;
                if (!controller.blackBoard.getItem(m_item.key, out t)) return BTResult.Failure;
                if (t != null) return BTResult.Success;
                break;
        }


        return BTResult.Failure;
    }
}
