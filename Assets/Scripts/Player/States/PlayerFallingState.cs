using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerInAirState
{
    public PlayerFallingState(PlayerController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(controller.DashInput)
        {
            controller.ChangeState(controller.dashState);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Vector3 direction;
        direction = controller.transform.forward * controller.MovementInput.z;
        direction += controller.transform.right * controller.MovementInput.x;
        direction.Normalize();

        controller.AccelerateToSpeed(direction, controller.playerStats.airSpeed, controller.playerStats.airAccel * Time.fixedDeltaTime);
    }
}
