using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestAI : BTController
{
    public SceneQuery rangedQuery;
    public SceneQuery idleQuery;

    public Transform playerTarget;
    public float memoryTime;

    public float nearRange;

    public AgentSensor[] agentSensors;

    private float m_forgetPlayerTime;
    private bool m_playerLost;

    private NavMeshAgent m_agent;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();

        blackBoard.setItem("SelfAgent", new BBAgent(m_agent));
        blackBoard.setItem("PlayerVisible", new BBBool(false));

        root = new BTRoot(
                new BTSelectorNode(new List<BTNode>
                {
                    // attack
                    new BTSequencerNode(new List<BTNode>
                    {
                        new BTCheckBB("Target"),
                        new BTAgentStats(false),
                        new BTFace("Target", 30.0f),
                        new BTCheckBB("PlayerVisible"),
                        new BTInverter(new BTSequencerNode(new List<BTNode>
                        {
                            new BTInRange("Target", nearRange),
                            new BTAgentStats(true),
                        })),
                    }),
                    // move to proper location
                    new BTSequencerNode(new List<BTNode>
                    {
                        new BTCheckBB("Target"),
                        new BTAgentStats(true),
                        new BTSQNode(rangedQuery, "AttackLocation"),
                        new BTGoToPosition("AttackLocation", false),
                        new BTDelay(0.5f, "PlayerVisible", false),
                    }),
                    // idle
                    new BTSequencerNode(new List<BTNode>
                    {
                        new BTInverter(new BTCheckBB("Target")),
                        new BTInverter(new BTCheckBB("PlayerVisible")),
                        new BTSQNode(idleQuery, "IdleLocation"),
                        new BTPrint("Idle"),
                        new BTAgentStats(true),
                        new BTGoToPosition("IdleLocation", false),
                        new BTDelay(3.0f, "PlayerVisible", true),
                    }),
                })
            );

        root.setController(this);
        SetEvaluatingNode(root);
    }

    private void Update()
    {
        if(Time.time >= m_forgetPlayerTime && m_playerLost)
        {
            blackBoard.setItem("Target", null);
            m_playerLost = false;
            print("Player Lost");
        }
    }

    public override void SensorObjectEnter(Transform newObject)
    {
        if(newObject == playerTarget)
        {
            blackBoard.setItem("Target", new BBTransform(playerTarget));
            blackBoard.setItem("PlayerVisible", new BBBool(true));
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
                blackBoard.setItem("PlayerVisible", new BBBool(false));
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
