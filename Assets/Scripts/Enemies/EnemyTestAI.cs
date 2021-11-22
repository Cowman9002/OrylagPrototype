using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestAI : BTController
{
    public SceneQuery rangedQuery;


    public Transform playerTarget;
    public float memoryTime;

    public float nearRange, farRange;

    public AgentSensor[] agentSensors;

    private float m_forgetPlayerTime;
    private bool m_playerLost;

    public Transform targetTransform;

    private NavMeshAgent m_agent;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();

        blackBoard.addItem("SelfAgent", m_agent);
        blackBoard.addItem("AttackPosition", targetTransform);

        root = new BTRoot(
                new BTSelectorNode(new List<BTNode>
                {
                    new BTSequencerNode(new List<BTNode> {
                        new BTPrint("Attack"),
                        new BTCheckBB(new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform)),
                        new BTAgentStats(false),
                        new BTFace(new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), 10.0f),

                        new BTSelectorNode(new List<BTNode> {
                            new BTInRange(new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), nearRange),
                            new BTInverter(new BTInRange(new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), farRange)),
                        }),

                        new BTSQNode(rangedQuery, "AttackPosition"),
                        new BTGoToPosition(new AIBlackBoard.BlackBoardElement("AttackPosition", AIBlackBoard.BlackBoardElement.ElementType.Transform), true),
                    }),
                    new BTSequencerNode(new List<BTNode> {
                        new BTPrint("Idle"),
                        new BTAgentStats(true),
                    }),
                })
            );

        root.setController(this);
    }

    private void Update()
    {
        if(Time.time >= m_forgetPlayerTime && m_playerLost)
        {
            blackBoard.addItem("Target", null);
            m_playerLost = false;
            print("Player Lost");
        }
    }

    public override void SensorObjectEnter(Transform newObject)
    {
        //print(newObject + " Is visible");
        if(newObject == playerTarget)
        {
            blackBoard.addItem("Target", playerTarget);
            m_playerLost = false;
        }
    }
    public override void SensorObjectExit(Transform newObject)
    {
        if (newObject == playerTarget)
        {
            if (!CheckForTransform(newObject))
            {
                m_forgetPlayerTime = Time.time + memoryTime;
                m_playerLost = true;
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

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, nearRange);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, farRange);
    }
}
