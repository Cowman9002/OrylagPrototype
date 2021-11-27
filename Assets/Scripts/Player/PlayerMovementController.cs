using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform viewTransform;
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
    [HideInInspector]
    public PlayerClimbingState climbState;

    public int numJumpsDone = 0;

    public Vector3 MovementInput { get; private set; }
    public Vector2 MouseInput { get; private set; }
    public bool JumpInput { get; private set; }
    private float m_jumpInputExpire;
    public bool DashInput { get; private set; }
    private float m_dashInputExpire;

    private float m_canDashTime;

    public bool IsGrounded { get; private set; }

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;

    public Vector3 Velocity { get; set; }

    // Components
    private CharacterController m_controller;
    private AudioSource m_audio;

    private PlayerState m_currentState;

    public void ChangeState(PlayerState newState)
    {
        m_currentState.OnExit();
        m_currentState = newState;
        m_currentState.OnEnter();
    }

    public void PlaySound(AudioClip sound)
    {
        if(m_audio.isPlaying)
        {
            m_audio.Stop();
        }

        m_audio.clip = sound;
        m_audio.Play();
    }

    public void UseJump()
    {
        JumpInput = false;
        m_jumpInputExpire = 0;
        IsGrounded = false;
    }

    public void UseDash()
    {
        DashInput = false;
        m_dashInputExpire = 0;

        m_canDashTime = Time.time + playerStats.dashCooldown;
    }

    public bool CanDash() => m_canDashTime <= Time.time;

    public void AccelerateToSpeed(Vector3 direction, float velocity, float acceleration)
    {
        float veloDelta = velocity - Vector3.Dot(Velocity, direction);
        float appliedSpeed = velocity * acceleration;

        if (veloDelta <= 0.0f) return;

        appliedSpeed = Mathf.Min(appliedSpeed, veloDelta);


        Velocity += direction * appliedSpeed;
    }

    public void SetPosition(Vector3 newPos)
    {
        m_controller.Move(newPos - transform.position);
    }

    public void RotateYaw(float amount)
    {
        m_yaw = (m_yaw + amount) % 360.0f;
    }

    public void RotatePitch(float amount)
    {
        m_pitch = Mathf.Clamp(m_pitch + amount, playerStats.minYaw, playerStats.maxYaw);
    }

    public float GetYaw()
    {
        return m_yaw;
    }

    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_audio = GetComponent<AudioSource>();

        MovementInput = new Vector2();

        // Create States
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        fallingState = new PlayerFallingState(this);
        jumpState = new PlayerJumpState(this);
        dashState = new PlayerDashState(this);
        climbState = new PlayerClimbingState(this);

        m_currentState = idleState;
        m_currentState.OnEnter();

        Cursor.lockState = CursorLockMode.Locked;

        if(playerStats.baseFOV == 0)
        {
            playerStats.baseFOV = viewCamera.fieldOfView;
        }
    }

    private void Update()
    {
        UpdateInput();
        m_currentState.OnUpdate();

        // apply transforms
        transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up);
        viewTransform.localRotation = Quaternion.AngleAxis(-m_pitch, Vector3.right);

        float targFov = Mathf.Lerp(playerStats.baseFOV, playerStats.maxFOV, Velocity.magnitude / playerStats.maxFOVSpeed);
        viewCamera.fieldOfView = Mathf.Lerp(viewCamera.fieldOfView, targFov, Time.deltaTime * playerStats.FOVChangeSpeed);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up * playerStats.climbLowHeight, transform.forward * playerStats.climbDist);
        Gizmos.DrawRay(transform.position + Vector3.up * playerStats.climbHighHeight, transform.forward * playerStats.climbDist);
    }
}
