using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerLookState
{
    public PlayerJumpState(PlayerController controller) : base(controller) { }

    public override void OnEnter()
    {
        controller.UseJump();
        controller.Velocity = new Vector3(controller.Velocity.x, controller.playerStats.jumpPower, controller.Velocity.z);
        controller.ChangeState(controller.inAirState);
    }
}
