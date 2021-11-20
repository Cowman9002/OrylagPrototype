using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Camera viewCamera;

    [HideInInspector]
    public PlayerIdleState idleState;
    [HideInInspector]
    public PlayerRunState runState;
    [HideInInspector]
    public PlayerFallingState fallingState;
    [HideInInspector]
    public PlayerJumpState jumpState;
    [HideInInspector]
    public PlayerDashState dashState;

    public Vector3 MovementInput { get; private set; }
    public Vector2 MouseInput { get; private set; }
    public bool JumpInput { get; private set; }
    private float m_jumpInputExpire;
    public bool DashInput { get; private set; }
    private float m_dashInputExpire;

    public bool IsGrounded { get; private set; }

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;

    public Vector3 Velocity { get; set; }

    // Components
    private CharacterController m_controller;

    private PlayerState m_currentState;

    public void ChangeState(PlayerState newState)
    {
        m_currentState.OnExit();
        m_currentState = newState;
        m_currentState.OnEnter();
    }

    public void UseJump()
    {
        JumpInput = false;
        m_jumpInputExpire = 0;
        IsGrounded = false;
    }

    public void AccelerateToSpeed(Vector3 direction, float velocity, float acceleration)
    {
        float veloDelta = velocity - Vector3.Dot(Velocity, direction);
        float appliedSpeed = velocity * acceleration;

        if (veloDelta <= 0.0f) return;

        appliedSpeed = Mathf.Min(appliedSpeed, veloDelta);


        Velocity += direction * appliedSpeed;
    }

    public void RotateYaw(float amount)
    {
        m_yaw = (m_yaw + amount) % 360.0f;
    }

    public void RotatePitch(float amount)
    {
        m_pitch = Mathf.Clamp(m_pitch + amount, playerStats.minYaw, playerStats.maxYaw);
    }

    public void SetFov(float amount)
    {
        viewCamera.fieldOfView = amount;
    }
    public float GetFov()
    {
        return viewCamera.fieldOfView;
    }

    private void Start()
    {
        m_controller = GetComponent<CharacterController>();

        MovementInput = new Vector2();

        // Create States
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        fallingState = new PlayerFallingState(this);
        jumpState = new PlayerJumpState(this);
        dashState = new PlayerDashState(this);

        m_currentState = idleState;
        m_currentState.OnEnter();

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        UpdateInput();
        m_currentState.OnUpdate();

        // apply transforms
        transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up);
        viewCamera.transform.localRotation = Quaternion.AngleAxis(-m_pitch, Vector3.right);
    }

    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
        m_controller.Move(Velocity * Time.fixedDeltaTime);
        IsGrounded = m_controller.isGrounded;
    }

    private void UpdateInput()
    {
        MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        MovementInput.Normalize();

        MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if(!JumpInput && Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
            m_jumpInputExpire = Time.time + playerStats.inputBufferTime;
        }
        else if(Time.time >= m_jumpInputExpire)
        {
            JumpInput = false;
        }

        if (!DashInput && Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashInput = true;
            m_dashInputExpire = Time.time + playerStats.inputBufferTime;
        }
        else if (Time.time >= m_dashInputExpire)
        {
            DashInput = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        m_currentState.OnControllerCollision(hit);
    }
}
