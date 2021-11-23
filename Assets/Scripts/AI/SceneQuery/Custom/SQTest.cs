using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQTest : SceneQuery
{
    public float nearRange;
    public float farRange;

    public LayerMask rayMask;
    public string targetName;

    void Start()
    {
        nodes.Add(new SQInRangeNode(this, targetName, nearRange, true));
        nodes.Add(new SQInRangeNode(this, targetName, farRange, false));
        nodes.Add(new SQDistanceNode(this, targetName, false));
        nodes.Add(new SQDistanceNode(this, null, true));
        nodes.Add(new SQDotProduct(this, targetName, false, false, 0.1f));
        nodes.Add(new SQVisibleNode(this, targetName, rayMask, 0.2f, false));
    }
}
