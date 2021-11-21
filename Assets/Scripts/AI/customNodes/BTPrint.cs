using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPrint : BTNode
{
    private string m_message;

    public BTPrint(string message) => m_message = message;

    public override BTResult Evaluate()
    {
        Debug.Log(m_message);
        return BTResult.Success;
    }
}
