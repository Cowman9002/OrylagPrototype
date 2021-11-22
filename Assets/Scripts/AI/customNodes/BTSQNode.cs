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
        Transform t;
        if (!controller.blackBoard.getItem(m_storage, out t)) return BTResult.Failure;

        Vector3 position;
        if(!m_query.Evaluate(out position))
        {
            return BTResult.Failure;
        }

        t.position = position;

        return BTResult.Success;
    }
}
