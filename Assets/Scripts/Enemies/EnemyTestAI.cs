using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestAI : BTController
{
    public Transform playerTarget;

    public AgentSensor[] agentSensors;

    private void Start()
    {
        blackboard.Add("SelfAgent", GetComponent<NavMeshAgent>());
        blackboard.Add("MainTarget", playerTarget);

        root = new BTRoot(
                new BTSelectorNode(new List<BTNode>
                {
                    new BTSequencerNode(new List<BTNode> {
                        new BTHasBBItem("Target"),
                        new BTGoToPosition("Target", true)
                    })
                })
            );

        root.setController(this);
    }

    public override void SensorObjectEnter(Transform newObject)
    {
        print(newObject + " Is visible");
        if(newObject == playerTarget)
        {
            blackboard["Target"] = playerTarget;
        }
    }
    public override void SensorObjectExit(Transform newObject)
    {
        if (newObject == playerTarget)
        {
            if (!CheckForTransform(newObject))
            {
                print(newObject + " Has vanished");
                blackboard.Remove("Target");
            }
        }
    }

    private bool CheckForTransform(Transform t)
    {
        foreach (AgentSensor a in agentSensors)
        {
            if (a.hasTransform(playerTarget))
            {
                return true;
            }
        }

        return false;
    }
}
