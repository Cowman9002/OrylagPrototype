using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTDecorator
{
    public BTInverter(BTNode child) : base(child) { }

    public override BTResult Evaluate()
    {
        switch(child.Evaluate())
        {
            case BTResult.Failure: return BTResult.Success;
            case BTResult.Success: return BTResult.Failure;
            case BTResult.Running: return BTResult.Running;
        }

        return BTResult.Failure;
    }
}
