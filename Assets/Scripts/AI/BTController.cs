using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BTController : MonoBehaviour
{
    public AIBlackBoard blackBoard;

    [HideInInspector]
    public NavMeshAgent agentSelf;

    [HideInInspector]
    public Health healthSelf;

    protected Transform lookTarget = null;

    protected BTNode root;
    private Stack<BTNode> m_evaluatingNodes = new Stack<BTNode>();

    protected virtual void Start()
    {
        agentSelf = GetComponent<NavMeshAgent>();
        healthSelf = GetComponent<Health>();
    }

    public virtual void FixedUpdate()
    {
        if (lookTarget != null)
        {
            Vector3 dir = lookTarget.position - transform.position;
            dir.y = 0.0f;
            Quaternion targRot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targRot, Time.fixedDeltaTime * 30.0f);
        }

        //print("Running " + m_evaluatingNodes.Peek().Name);
        m_evaluatingNodes.Peek().Evaluate();
    }

    public virtual void SensorObjectEnter(Transform newObject) { }
    public virtual void SensorObjectExit(Transform newObject) { }

    public void SetLookTarget(Transform t)
    {
        lookTarget = t;
        agentSelf.updateRotation = t == null;
    }

    public void BeginState(BTNode node)
    {
        m_evaluatingNodes.Push(node);

        //printDebugStartMessage(node);
    }

    public struct BTStateEndData
    {
        public BTNode.BTResult result;
    }

    public BTStateEndData EndState(BTNode.BTResult result)
    {
        BTStateEndData res;
        res.result = result;

        if (m_evaluatingNodes.Count == 1 || result == BTNode.BTResult.Running) return res;

        BTNode node = m_evaluatingNodes.Pop();
        m_evaluatingNodes.Peek().ChildEnded(res);

        //PrintDebugEndMessage(node, result);

        return res;
    }

    private void printDebugStartMessage(BTNode node)
    {
        string stack = node.Name + " has begun : { ";

        foreach (BTNode n in m_evaluatingNodes)
        {
            stack += n.Name + ", ";
        }

        stack += " }";
        print(stack);
    }

    private void PrintDebugEndMessage(BTNode node, BTNode.BTResult result)
    {
        string stack = node.Name + " has ended with " + result + " : { ";

        foreach (BTNode n in m_evaluatingNodes)
        {
            stack += n.Name + ", ";
        }

        stack += " }";
        print(stack);
    }
}
