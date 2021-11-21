using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSucceed : BTNode
{
    public override BTResult Evaluate()
    {
        return BTResult.Success;
    }
}