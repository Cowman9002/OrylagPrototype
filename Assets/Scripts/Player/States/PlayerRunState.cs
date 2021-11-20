using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerMovementController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.MovementInput.sqrMagnitude == 0.0f)
        {
            controller.ChangeState(controller.idleState);
            return;
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Vector3 direction;
        direction = controller.transform.forward * controller.MovementInput.z;
        direction += controller.transform.right * controller.MovementInput.x;

        direction.Normalize();

        controller.AccelerateToSpeed(direction, controller.playerStats.movementSpeed, controller.playerStats.runAccel * Time.fixedDeltaTime);
    }
}
