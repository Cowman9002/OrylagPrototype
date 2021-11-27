using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI: BTController
{
    public LayerMask raycastMask;

    public GameObject hurtBox;

    public Animator animator;

    public SceneQuery rangedQuery;
    public SceneQuery idleQuery;

    public Transform playerTarget;
    public float memoryTime;

    public float nearRange;
    public float farRange;

    public AgentSensor[] agentSensors;

    private float m_forgetTime;

    protected override void Start()
    {
        base.Start();

        blackBoard.setItem("Enemy", new BBTransform(playerTarget));
        blackBoard.setItem("EnemyVisible", new BBBool(false));
        blackBoard.setItem("EnemyWasSeen", new BBBool(false));
        blackBoard.setItem("CanAttack", new BBBool(false));

        root = new BTSelectorNode("Root", new List<BTNode>
                {
                    new BTSelectorNode("Attack", new List<BTNode>
                    {
                        new BTSequencerNode("Attack", new List<BTNode>
                        {
                            new BTCheckBB(null, "EnemyWasSeen"),
                            new BTCheckBB(null, "CanAttack"),
                            new BTSetLookTarget(null, "Enemy"),
                            new BTAnimate(null, animator, "Attack"),
                            new BTDelay(null, 1.0f, "CanAttack", false),
                        }),
                        new BTSequencerNode("GoToAttackLocation", new List<BTNode>
                        {
                            new BTCheckBB(null, "EnemyWasSeen"),
                            new BTSetLookTarget(null, null),
                            new BTSelectorNode("Select Attack location", new List<BTNode>
                            {
                                new BTSQNode(null, rangedQuery, "TargetLocation"),
                                new BTRandomLocation(null, "Enemy", farRange, "TargetLocation"),
                            }),
                            new BTGoToPosition(null, "TargetLocation", false),
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

    public override void FixedUpdate()
    {

        bool hasHitPlayer = false;
        bool inRange;

        float sqrDst = Vector3.SqrMagnitude(playerTarget.position - transform.position);
        inRange = sqrDst > nearRange * nearRange && sqrDst < farRange * farRange;

        Vector3 rayOrigin = transform.position + Vector3.up * 1.7f;
        Vector3 rayDirection = (playerTarget.position - rayOrigin).normalized;
        RaycastHit hit;

        if(Physics.Raycast(new Ray(rayOrigin, rayDirection), out hit, farRange, raycastMask))
        {
            if (hit.transform == playerTarget)
            {
                hasHitPlayer = true;
            }
        }

        if(hasHitPlayer && inRange)
        {
            blackBoard.setItem("CanAttack", new BBBool(true));
        }
        else
        {
            blackBoard.setItem("CanAttack", new BBBool(false));
        }

        if(!hasHitPlayer)
        {
            m_forgetTime = Time.time + memoryTime;
        }
        else
        {
            m_forgetTime = 0;
        }

        if (Time.time >= m_forgetTime && m_forgetTime != 0)
        {
            blackBoard.setItem("EnemyWasSeen", new BBBool(false));
        }

        base.FixedUpdate();
    }

    public override void SensorObjectEnter(Transform newObject)
    {
        if(newObject == playerTarget)
        {
            blackBoard.setItem("EnemyVisible", new BBBool(true));
            blackBoard.setItem("CanAttack", new BBBool(true));
            blackBoard.setItem("EnemyWasSeen", new BBBool(true));
        }
    }
    public override void SensorObjectExit(Transform newObject)
    {
        if (newObject == playerTarget)
        {
            if (!CheckForTransform(newObject))
            {
                blackBoard.setItem("EnemyVisible", new BBBool(false));
            }
        }
    }

    private bool CheckForTransform(Transform t)
    {
        foreach (AgentSensor a in agentSensors)
        {
            if (a.hasTransform(t))
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            healthSelf.DecreaseHealth(other.GetComponent<Bullet>().stats.damage, other.transform.position, -other.transform.forward, "Player");

            blackBoard.setItem("EnemyWasSeen", new BBBool(true));
            blackBoard.setItem("EnemyVisible", new BBBool(true));

            if (healthSelf.IsDead)
            {
                Destroy(gameObject);
            }
        }
    }

    public void EnableHurtBox()
    {
        hurtBox.SetActive(true);
    }

    public void DisableHurtBox()
    {
        hurtBox.SetActive(false);
    }
}
