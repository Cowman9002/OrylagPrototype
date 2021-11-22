using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTController : MonoBehaviour
{
    public AIBlackBoard blackBoard;

    protected BTRoot root;
    private BTNode m_evalulatingNode;

    public void FixedUpdate()
    {
        print(m_evalulatingNode);
        m_evalulatingNode.Evaluate();
    }

    public virtual void SensorObjectEnter(Transform newObject) { }
    public virtual void SensorObjectExit(Transform newObject) { }

    public void SetEvaluatingNode(BTNode node) => m_evalulatingNode = node;
    public void FinishState() => m_evalulatingNode = root;
}
