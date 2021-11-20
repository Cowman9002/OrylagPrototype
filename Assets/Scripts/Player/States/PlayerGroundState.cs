using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerLookState
{
    public PlayerGroundState(PlayerController controller) : base(controller) { }

    public override void OnEnter()
    {
        controller.Velocity = new Vector3(controller.Velocity.x, 0.0f, controller.Velocity.z);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(!controller.IsGrounded)
        {
            controller.ChangeState(controller.fallingState);
        }
        else if(controller.JumpInput)
        {
            controller.ChangeState(controller.jumpState);
            return;
        }
        else if (controller.DashInput)
        {
            controller.ChangeState(controller.dashState);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        controller.Velocity = controller.Velocity - controller.Velocity * controller.playerStats.groundFriction;
    }
}
