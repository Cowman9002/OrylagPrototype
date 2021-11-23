using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPrint : BTNode
{
    private string m_message;

    public BTPrint(string name, string message) : base(name) => m_message = message;

    public override BTController.BTStateEndData Evaluate()
    {
        Debug.Log(m_message);
        return controller.EndState(BTResult.Success);
    }
}
