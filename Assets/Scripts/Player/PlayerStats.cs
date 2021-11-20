using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public LayerMask layer;

    public float gravity;
    public float inputBufferTime;

    [Header("Speeds")]
    public float movementSpeed;
    public float airSpeed;
    public float rotateSpeed;


    [Header("Accelerations")]
    public float airAccel;
    public float runAccel;

    [Header("Jump")]
    public float jumpPower;

    [Header("Constraints")]
    public float maxYaw;
    public float minYaw;

    [Header("Friction")]
    public float groundFriction;

    [Header("Dash")]
    public AnimationCurve dashCurve;
    public float dashFovDelta;
    public float dashTime;
    public float dashSpeed;
}
