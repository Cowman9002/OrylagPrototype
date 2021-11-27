using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerLookState
{
    public PlayerDashState(PlayerMovementController controller) : base(controller) { }

    private float m_endTime;

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
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.JumpInput && controller.numJumpsDone < controller.playerStats.numJumps)
        {
            controller.ChangeState(controller.jumpState);
            return;
        }

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
