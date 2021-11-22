using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackBoardItem
{
    public enum EType
    {
        Bool, Vector, Transform, Agent, INVALID
    }

    public EType Type { get; private set; }

    protected BlackBoardItem(EType type)
    {
        Type = type;
    }
}

public class BBBool : BlackBoardItem
{
    public bool value;

    public BBBool(bool value) : base(EType.Bool) => this.value = value;
}

public class BBVector : BlackBoardItem
{
    public Vector3 value;

    public BBVector(Vector3 value) : base(EType.Vector) => this.value = value;
}

public class BBTransform : BlackBoardItem
{
    public Transform value;

    public BBTransform(Transform value) : base(EType.Transform) => this.value = value;
}

public class BBAgent : BlackBoardItem
{
    public NavMeshAgent value;

    public BBAgent(NavMeshAgent value) : base(EType.Agent) => this.value = value;
}

