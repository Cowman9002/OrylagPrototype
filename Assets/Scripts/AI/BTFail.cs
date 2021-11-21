using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFail : BTNode
{
    public override BTResult Evaluate()
    {
        return BTResult.Failure;
    }
}
