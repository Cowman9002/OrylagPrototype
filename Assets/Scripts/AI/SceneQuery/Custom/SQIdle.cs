﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQIdle : SceneQuery
{
    void Start()
    {
        nodes.Add(new SQVisibleNode(this, null, 0.5f, false));
        nodes.Add(new SQRandomNode(this, 0.0f, 1.0f));
        nodes.Add(new SQDotProduct(this, null, true, true, 0.3f));
    }
}
