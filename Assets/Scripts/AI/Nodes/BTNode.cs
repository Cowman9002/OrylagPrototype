using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public string Name { get; private set; }

    public enum BTResult
    {
        Running, Success, Failure
    }

    public BTController controller;
    public BTNode parent;

    public BTNode(string name)
    {
        Name = name;
    }

    public void ValidateName()
    {
        if(Name == null)
        {
            Name = parent.Name + "." + ToString();
        }
        else
        {
            Name = parent.Name + "." + Name;
        }

    }


    public virtual void setController(BTController controller) { this.controller = controller; }

    public virtual BTController.BTStateEndData Evaluate() { return controller.EndState(BTResult.Failure); }

    public virtual void ChildEnded(BTController.BTStateEndData data) { }
}
