using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAlwaysSucceed : BTDecorator
{
    public BTAlwaysSucceed(BTNode child) : base(child) { }

    public override BTResult Evaluate()
    {
        child.Evaluate();
        return BTResult.Success;
    }
}
