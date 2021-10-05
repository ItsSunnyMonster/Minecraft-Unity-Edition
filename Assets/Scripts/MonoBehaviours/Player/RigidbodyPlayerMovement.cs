// 
// Copyright (c) SunnyMonster
//

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMovement : MonoBehaviour
{ 
    [Header("Options")]
    [SerializeField] private float jumpHeight = 1.25f;
    [SerializeField] private float walkVelocity = 4.317f;
    [SerializeField] private float sprintVelocity = 5.612f;
    [SerializeField] private float crouchVelocity = 1.31f;
    [SerializeField] [Range(0f, .3f)] private float accelerationSmoothing = 0.3f;
    [SerializeField] [Range(0f, .3f)] private float decelerationSmoothing = 0.1f;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpCooldown;
    [Space]
    [Header("References")]
    [SerializeField] private Transform playerGFX;
    [SerializeField] private BoxCollider hitbox;
    [SerializeField] private new Transform camera;

    private Rigidbody _rb;

    private Vector2 _velocity;
    private bool _onGround;

    private float FeetDistance
    {
        get
        {
            return hitbox.size.y / 2f - 0.01f;
        }
    }

    private Vector3 FeetPos
    {
        get
        {
            return new Vector3(hitbox.center.x, hitbox.center.y - FeetDistance, hitbox.center.z) + transform.position;
        }
    }

    private Vector3 CubeCheckPos
    {
        get
        {
            return new Vector3(FeetPos.x, FeetPos.y - groundDistance / 2f, FeetPos.z);
        }
    }

    private Vector3 CubeCheckSize
    {
        get
        {
            return new Vector3(hitbox.size.x, groundDistance, hitbox.size.z);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpCoolDownTimer = jumpCooldown;
    }

    private float _jumpCoolDownTimer;

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        var jumpInput = Input.GetButton("Jump");

        _onGround = Physics.CheckBox(CubeCheckPos, CubeCheckSize / 2f, Quaternion.identity, ground);

        var velocity = walkVelocity;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            velocity = sprintVelocity;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = crouchVelocity;
        }
        

        var move = horizontalInput * walkVelocity * playerGFX.right + verticalInput * velocity * playerGFX.forward;
        Move(move.x, move.z, jumpInput);

        _jumpCoolDownTimer += Time.deltaTime;

        if (Input.GetButtonUp("Jump"))
        {
            _jumpCoolDownTimer = jumpCooldown;
        }
    }

    public void Move(float velX, float velZ, bool jump)
    {
        // Calculate the target velocity
        var targetVelocity = new Vector2(velX, velZ);

        var currentVelocity = new Vector2(_rb.velocity.x, _rb.velocity.z);

        Vector2 smoothedVelocity;

        // Smooth the velocity change and set the velocity
        if (currentVelocity.magnitude > targetVelocity.magnitude)
        {
            smoothedVelocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref _velocity, decelerationSmoothing);
        }
        else
        {
            smoothedVelocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref _velocity, accelerationSmoothing);
        }

        _rb.velocity = new Vector3(smoothedVelocity.x, _rb.velocity.y, smoothedVelocity.y);

        // If not should jump then do nothing
        if (!jump || !_onGround) return;
        if (!(_jumpCoolDownTimer >= jumpCooldown)) return;
        // Add jumping velocity
        _rb.velocity = new Vector3(_rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), _rb.velocity.z);
        _jumpCoolDownTimer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(CubeCheckPos, CubeCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(FeetPos, new Vector3(hitbox.size.x, 0f, hitbox.size.z));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(camera.position, new Vector3(hitbox.size.x, 0f, hitbox.size.z));
        Gizmos.color = Color.black;
        Gizmos.DrawRay(camera.position, camera.forward * 2f);
    }
}