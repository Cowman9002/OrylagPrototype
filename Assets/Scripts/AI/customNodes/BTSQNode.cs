using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSQNode : BTNode
{
    private SceneQuery m_query;
    private string m_storage;

    public BTSQNode(SceneQuery query, string storageName)
    {
        m_query = query;
        m_storage = storageName;
    }

    public override BTResult Evaluate()
    {
        Vector3 position;
        if(!m_query.Evaluate(out position))
        {
            return BTResult.Failure;
        }

        controller.blackBoard.setItem(m_storage, new BBVector(position));

        return BTResult.Success;
    }
}
