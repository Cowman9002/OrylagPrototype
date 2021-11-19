using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookState : PlayerState
{
    public PlayerLookState(PlayerController controller) : base(controller) { }

    public override void OnUpdate()
    {
        controller.RotateYaw(controller.playerStats.rotateSpeed * controller.MouseInput.x);
        controller.RotatePitch(controller.playerStats.rotateSpeed * controller.MouseInput.y);
    }
}
