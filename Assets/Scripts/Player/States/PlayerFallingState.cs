using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerInAirState
{
    public PlayerFallingState(PlayerMovementController controller) : base(controller) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(controller.DashInput && controller.CanDash())
        {
            controller.ChangeState(controller.dashState);
        }
    }

    public override void OnFixedUpdate()
    {
        if(AttemptClimb()) return;

        base.OnFixedUpdate();

        Vector3 direction;
        direction = controller.transform.forward * controller.MovementInput.z;
        direction += controller.transform.right * controller.MovementInput.x;
        direction.Normalize();

        controller.AccelerateToSpeed(direction, controller.playerStats.airSpeed, controller.playerStats.airAccel * Time.fixedDeltaTime);
    }

    private bool AttemptClimb()
    {
        Ray bottomRay = new Ray(controller.transform.position + Vector3.up * controller.playerStats.climbLowHeight, controller.transform.forward);
        Ray topRay = new Ray(controller.transform.position + Vector3.up * controller.playerStats.climbHighHeight, controller.transform.forward);

        int layer = 1 << LayerMask.NameToLayer("Level");
        bool bottomRaycast = Physics.Raycast(bottomRay, controller.playerStats.climbDist, layer);
        bool topRaycast = Physics.Raycast(topRay, controller.playerStats.climbDist, layer);

        if (bottomRaycast && !topRaycast)
        {
            controller.ChangeState(controller.climbState);
            return true;
        }

        return false;
    }
}
