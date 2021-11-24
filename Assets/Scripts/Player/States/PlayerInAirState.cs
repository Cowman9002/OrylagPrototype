using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerLookState
{
    public PlayerInAirState(PlayerMovementController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(controller.IsGrounded)
        {
            if(controller.MovementInput.sqrMagnitude > 0.0f)
            {
                controller.ChangeState(controller.runState);
            }
            else
            {
                controller.ChangeState(controller.idleState);
            }
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        controller.Velocity = controller.Velocity + Vector3.up * controller.playerStats.gravity * Time.fixedDeltaTime;
    }


}
