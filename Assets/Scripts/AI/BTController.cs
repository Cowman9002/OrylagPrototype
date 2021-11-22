using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTController : MonoBehaviour
{
    public AIBlackBoard blackBoard;

    protected BTRoot root;

    public void FixedUpdate()
    {
        root.Evaluate();
    }

    public virtual void SensorObjectEnter(Transform newObject) { }
    public virtual void SensorObjectExit(Transform newObject) { }
}
