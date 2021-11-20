using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerLookState
{
    public PlayerDashState(PlayerMovementController controller) : base(controller) { }

    private float m_endTime;

    private float m_originalFov;

    private float m_td;
    private float m_t;

    public override void OnEnter()
    {
        base.OnEnter();
        controller.UseDash();

        controller.PlaySound(controller.playerStats.dashSound);

        Vector3 direction;
        direction = controller.transform.forward * controller.MovementInput.z;
        direction += controller.transform.right * controller.MovementInput.x;
        direction.Normalize();

        if(direction.sqrMagnitude == 0.0f)
        {
            direction = controller.transform.forward;
        }

        controller.Velocity = direction * controller.playerStats.dashSpeed;

        m_endTime = Time.time + controller.playerStats.dashTime;
        m_originalFov = controller.GetFov();

        m_td = 1.0f / controller.playerStats.dashTime;
        m_t = 0.0f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_t += m_td * Time.deltaTime;
        float fov = m_originalFov + controller.playerStats.dashCurve.Evaluate(m_t) * controller.playerStats.dashFovDelta;
        controller.SetFov(fov);

        if (Time.time >= m_endTime)
        {
            ExitState();
        }
    }

    public override void OnControllerCollision(ControllerColliderHit hit)
    {
        base.OnControllerCollision(hit);

        if(Vector3.Dot(hit.normal, controller.Velocity.normalized) < -0.7f)
        {
            controller.Velocity = Vector3.zero;
            ExitState();
        }
    }

    private void ExitState()
    {
        controller.SetFov(m_originalFov);
        if (controller.IsGrounded)
        {
            controller.ChangeState(controller.idleState);
        }
        else
        {
            controller.ChangeState(controller.fallingState);
        }
    }
}
