using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAlwaysFail : BTDecorator
{
    public BTAlwaysFail(BTNode child) : base(child) { }

    public override BTResult Evaluate()
    {
        child.Evaluate();
        return BTResult.Failure;
    }
}
