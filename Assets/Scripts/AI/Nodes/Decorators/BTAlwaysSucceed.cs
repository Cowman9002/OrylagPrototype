using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAlwaysSucceed : BTDecorator
{
    public BTAlwaysSucceed(string name, BTNode child) : base(name, child) { }

    public override BTController.BTStateEndData Evaluate()
    {
        controller.BeginState(child);
        child.Evaluate();

        return controller.EndState(BTResult.Success);
    }
}
