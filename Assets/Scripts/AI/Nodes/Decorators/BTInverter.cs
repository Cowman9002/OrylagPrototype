using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTDecorator
{
    public BTInverter(string name, BTNode child) : base(name, child) { }

    public override BTController.BTStateEndData Evaluate()
    {
        controller.BeginState(child);
        switch(child.Evaluate().result)
        {
            case BTResult.Failure: return controller.EndState(BTResult.Success);
            case BTResult.Success: return controller.EndState(BTResult.Failure);
            case BTResult.Running: return controller.EndState(BTResult.Running);
        }

        return controller.EndState(BTResult.Failure);
    }
}
