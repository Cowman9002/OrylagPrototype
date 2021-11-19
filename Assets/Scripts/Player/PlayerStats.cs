using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public LayerMask layer;

    public float gravity;

    [Header("Speeds")]
    public float movementSpeed;
    public float airSpeed;
    public float rotateSpeed;


    [Header("Accelerations")]
    public float airAccel;
    public float runAccel;

    [Header("Jump")]
    public float jumpPower;
    public float jumpBufferTime;

    [Header("Constraints")]
    public float maxYaw;
    public float minYaw;

    [Header("Friction")]
    public float groundFriction;
}
