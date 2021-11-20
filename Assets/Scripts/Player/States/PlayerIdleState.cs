using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerMovementController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(controller.MovementInput.sqrMagnitude > 0)
        {
            controller.ChangeState(controller.runState);
        }
    }
}
