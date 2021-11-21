﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDecorator : BTNode
{
    protected BTNode child;
    public BTDecorator(BTNode child)
    {
        this.child = child;
        this.child.controller = controller;
    }

    public override void setController(BTController controller)
    {
        base.setController(controller);
        child.setController(controller);
    }
}
