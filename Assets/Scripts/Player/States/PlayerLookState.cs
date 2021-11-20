using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookState : PlayerState
{
    public PlayerLookState(PlayerMovementController controller) : base(controller) { }

    public override void OnUpdate()
    {
        controller.RotateYaw(controller.playerStats.rotateSpeed * controller.MouseInput.x);
        controller.RotatePitch(controller.playerStats.rotateSpeed * controller.MouseInput.y);
    }
}
