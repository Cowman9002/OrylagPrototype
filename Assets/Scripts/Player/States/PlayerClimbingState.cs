using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerState
{
    public PlayerClimbingState(PlayerMovementController controller) : base(controller) { }

    private Vector3 m_destPos;
    private Vector3 m_deltaFlat;
    private static float DISTANCE_ERR = 0.03f;

    private float m_startYaw;

    public override void OnEnter()
    {

        Ray ray = new Ray(controller.transform.position + Vector3.up * controller.playerStats.climbHighHeight + controller.transform.forward, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Level")))
        {
            m_destPos = hit.point;
        }
        else
        {
            controller.ChangeState(controller.fallingState);
        }

        m_startYaw = controller.GetYaw();

        m_deltaFlat = m_destPos - controller.transform.position;
        m_deltaFlat.y = 0.0f;

        controller.Velocity = Vector3.up * 5.0f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(m_destPos.y - controller.transform.position.y < DISTANCE_ERR)
        {
            controller.Velocity = m_deltaFlat * controller.playerStats.movementSpeed * 0.3f;

            if (controller.MovementInput.sqrMagnitude > 0.0f)
            {
                controller.ChangeState(controller.runState);
                return;
            }
            else if(controller.DashInput)
            {
                controller.ChangeState(controller.dashState);
                return;
            }
        }

        if(Vector3.Dot(m_deltaFlat, m_destPos - controller.transform.position) < 0.0f)
        {
            controller.ChangeState(controller.idleState);
            return;
        }

        

        float yawOffset = controller.playerStats.rotateSpeed * controller.MouseInput.x;
        if (Mathf.Abs(controller.GetYaw() + yawOffset - m_startYaw) < 110.0f)
        {
            controller.RotateYaw(yawOffset);
        }

        controller.RotatePitch(controller.playerStats.rotateSpeed * controller.MouseInput.y);
    }

}
