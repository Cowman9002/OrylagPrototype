using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerLookState
{
    public PlayerInAirState(PlayerController controller) : base(controller) { }

    public override void OnEnter()
    {
    }


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

        Vector3 direction;
        direction = controller.transform.forward * controller.MovementInput.z;
        direction += controller.transform.right * controller.MovementInput.x;
        direction.Normalize();

        controller.AccelerateToSpeed(direction, controller.playerStats.airSpeed, controller.playerStats.airAccel * Time.fixedDeltaTime);
    }
}
