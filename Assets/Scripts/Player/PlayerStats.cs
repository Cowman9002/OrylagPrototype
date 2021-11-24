using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public LayerMask layer;

    public float gravity;
    public float inputBufferTime;

    [Header("Sounds")]
    public AudioClip dashSound;

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
    public float dashCooldown;
    public AnimationCurve dashCurve;
    public float dashFovDelta;
    public float dashTime;
    public float dashSpeed;

    [Header("Climb")]
    public float climbLowHeight;
    public float climbHighHeight;
    public float climbDist;
}
