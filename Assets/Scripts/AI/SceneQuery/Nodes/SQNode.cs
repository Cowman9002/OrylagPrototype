using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQNode
{
    protected SceneQuery parent;

    public SQNode(SceneQuery parent) { this.parent = parent; }

    public virtual bool PerformQuery(ref List<SceneQuery.QueryPoint> points) { return false; }
}
