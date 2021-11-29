using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : SlopeClimbState
{
    public PlayerGroundState(PlayerMovementController controller) : base(controller) 
    {
    }

    public override void OnEnter()
    {
        controller.Velocity = new Vector3(controller.Velocity.x, 0.0f, controller.Velocity.z);
        controller.numJumpsDone = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(controller.JumpInput && controller.numJumpsDone < controller.playerStats.numJumps)
        {
            controller.ChangeState(controller.jumpState);
            return;
        }
        else if (controller.DashInput && controller.CanDash())
        {
            controller.ChangeState(controller.dashState);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Vector3 normal;
        float height;
        if(GetGroundNormal(out normal, out height))
        {
            controller.Velocity = CaclSlopeDirection(normal);
            SnapToGround(height);

            controller.Velocity = controller.Velocity - controller.Velocity * controller.playerStats.groundFriction;
        }
        else
        {
            controller.ChangeState(controller.fallingState);
        }
    }
}
