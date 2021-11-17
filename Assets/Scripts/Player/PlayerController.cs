using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;

    public Transform viewCamera;

    [HideInInspector]
    public PlayerIdleState idleState;
    [HideInInspector]
    public PlayerRunState runState;

    public Vector3 movementInput;
    public Vector2 mouseInput;

    private PlayerState m_currentState;

    public void ChangeState(PlayerState newState)
    {
        m_currentState.OnExit();
        m_currentState = newState;
        m_currentState.OnEnter();
    }

    public void Move(Vector3 movement)
    {
        transform.Translate(movement);
    }

    public void RotateYaw(float amount)
    {
        transform.Rotate(Vector3.up * amount);
    }

    public void RotatePitch(float amount)
    {
        viewCamera.Rotate(Vector3.right * amount);
    }

    private void Start()
    {
        movementInput = new Vector2();

        // Create States
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);

        m_currentState = idleState;
        m_currentState.OnEnter();

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        // Input
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");
        movementInput.Normalize();

        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");

        m_currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }
}
