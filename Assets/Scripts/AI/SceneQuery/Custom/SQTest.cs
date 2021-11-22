using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQTest : SceneQuery
{
    public float nearRange;
    public float farRange;

    void Start()
    {
        nodes.Add(new SQInRangeNode(this, "Target", nearRange, true));
        nodes.Add(new SQInRangeNode(this, "Target", farRange, false));
        nodes.Add(new SQDistanceNode(this, "Target", false));
        nodes.Add(new SQDistanceNode(this, null, true));
        nodes.Add(new SQDotProduct(this, "Target", false));
        nodes.Add(new SQVisibleNode(this, "Target", 0.5f, false));
    }
}
