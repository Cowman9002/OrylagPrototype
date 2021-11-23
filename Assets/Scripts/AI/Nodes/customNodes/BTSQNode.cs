using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSQNode : BTNode
{
    private SceneQuery m_query;
    private string m_storage;

    public BTSQNode(string name, SceneQuery query, string storageName) : base(name)
    {
        m_query = query;
        m_storage = storageName;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        Vector3 position;
        if(!m_query.Evaluate(out position))
        {
            return controller.EndState(BTResult.Failure);
        }

        controller.blackBoard.setItem(m_storage, new BBVector(position));

        return controller.EndState(BTResult.Success);
    }
}
