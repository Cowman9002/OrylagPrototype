using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{

    public PlayerState(PlayerMovementController controller) => this.controller = controller;

    protected PlayerMovementController controller;

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnControllerCollision(ControllerColliderHit hit) { }
}
