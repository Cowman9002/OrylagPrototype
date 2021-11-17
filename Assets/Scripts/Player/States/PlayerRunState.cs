using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.movementInput.sqrMagnitude < 0.1f)
        {
            controller.ChangeState(controller.idleState);
            return;
        }

        controller.Move(controller.movementInput * controller.playerStats.movementSpeed * Time.deltaTime);
    }
}
