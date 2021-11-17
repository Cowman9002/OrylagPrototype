using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerController controller) : base(controller) { }

    public override void OnUpdate()
    {
        //rotate player
        controller.RotateYaw(controller.playerStats.rotateSpeed * controller.mouseInput.x);
        controller.RotatePitch(controller.playerStats.rotateSpeed * -controller.mouseInput.y);
    }
}
