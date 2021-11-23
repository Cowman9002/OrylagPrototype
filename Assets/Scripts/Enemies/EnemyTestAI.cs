using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestAI : BTController
{
    public LayerMask raycastMask;

    public SceneQuery rangedQuery;
    public SceneQuery idleQuery;

    public Transform playerTarget;
    public float memoryTime;

    public float nearRange;
    public float farRange;

    public AgentSensor[] agentSensors;

    private float m_forgetPlayerTime;
    private bool m_playerLost;

    protected override void Start()
    {
        base.Start();

        blackBoard.setItem("EnemyVisible", new BBBool(false));

        root = new BTSelectorNode("Root", new List<BTNode>
                {
                    new BTSelectorNode("Attack", new List<BTNode>
                    {
                        new BTSequencerNode("Shoot", new List<BTNode>
                        {
                            new BTCheckBB(null, "Enemy"),
                            new BTRayCast(null, "Enemy", raycastMask, Vector3.up * 1.7f, farRange, 0.0f),
                            new BTInverter(null, new BTInRange(null, "Enemy", nearRange)),
                            new BTSetLookTarget(null, "Enemy"),
                            new BTPrint(null, "FIRE"),
                            new BTDelay(null, 1.0f, null, false),
                        }),
                        new BTSequencerNode("GoToAttackLocation", new List<BTNode>
                        {
                            new BTCheckBB(null, "Enemy"),
                            new BTSetLookTarget(null, null),
                            new BTSelectorNode("Select Attack location", new List<BTNode>
                            {
                                new BTSQNode(null, rangedQuery, "TargetLocation"),
                                new BTRandomLocation(null, "Enemy", 10.0f, "TargetLocation"),
                            }),
                            new BTGoToPosition(null, "TargetLocation", false),
                            new BTDelay(null, 0.1f, null, false),
                        }),
                    }),
                    new BTSequencerNode("Idle", new List<BTNode>
                    {
                        new BTSetLookTarget(null, null),
                        new BTSQNode(null, idleQuery, "TargetLocation"),
                        new BTGoToPosition(null, "TargetLocation", false),
                        new BTDelay(null, 4.0f, "EnemyVisible", true),
                    }),
                });

        root.setController(this);
        BeginState(root);
    }

    private void Update()
    {
        //if(Time.time >= m_forgetPlayerTime && m_playerLost)
        //{
        //    blackBoard.setItem("Enemy", null);
        //    m_playerLost = false;
        //    print("Player Lost");
        //}
    }

    public override void SensorObjectEnter(Transform newObject)
    {
        if(newObject == playerTarget)
        {
            blackBoard.setItem("Enemy", new BBTransform(playerTarget));
            blackBoard.setItem("EnemyVisible", new BBBool(true));
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
                blackBoard.setItem("EnemyVisible", new BBBool(false));
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
