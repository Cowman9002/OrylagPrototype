using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerLookState
{
    public PlayerJumpState(PlayerMovementController controller) : base(controller) { }

    public override void OnEnter()
    {
        controller.UseJump();
        if(controller.numJumpsDone > 0)
        {
            controller.PlaySound(controller.playerStats.doubleJumpSound);
        }
        controller.numJumpsDone += 1;
        controller.Velocity = new Vector3(controller.Velocity.x, controller.playerStats.jumpPower, controller.Velocity.z);
        controller.ChangeState(controller.fallingState);
    }
}
