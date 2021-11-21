using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public enum BTResult
    {
        Running, Success, Failure
    }


    public BTController controller;
    public BTNode parent;

    public virtual void setController(BTController controller) { this.controller = controller; }

    public virtual BTResult Evaluate() { return BTResult.Failure; }
}
