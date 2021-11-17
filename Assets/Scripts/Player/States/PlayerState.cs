using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{

    public PlayerState(PlayerController controller) => this.controller = controller;

    protected PlayerController controller;

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}
