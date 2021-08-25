// 
// Copyright (c) SunnyMonster
//

using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyCharacterController : MonoBehaviour
{
    [Header("Options")]
    [FormerlySerializedAs("jumpForce")] [SerializeField] private float jumpHeight = 1.1f;
    [SerializeField] [Range(0f, .3f)] private float accelerationSmoothing = 0.1f;
    [SerializeField] [Range(0f, .3f)] private float decelerationSmoothing = 0.1f;

    [Header("References")]
    [SerializeField] private Transform groundCheck;

    private Rigidbody _rb;

    private Vector3 _velocity;
    private bool _onGround;
    private bool _isJumping;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private bool _wasOnGroundLastFixedUpdate;
    private void FixedUpdate()
    {
        if (!_wasOnGroundLastFixedUpdate && _onGround && _isJumping)
        {
            _isJumping = false;
        }
        _wasOnGroundLastFixedUpdate = _onGround;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _onGround = false;
        }
    }

    public void Move(float moveX, float moveZ, bool jump)
    {
        // Calculate the target velocity
        var targetVelocity = new Vector3(moveX * 10f, _rb.velocity.y, moveZ * 10f);

        // Smooth the velocity change and set the velocity
        if (_rb.velocity.magnitude > targetVelocity.magnitude)
        {
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, decelerationSmoothing);
        }
        else if (_rb.velocity.magnitude < targetVelocity.magnitude)
        {
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, accelerationSmoothing);
        }

        // If not should jump then do nothing
        if (!jump || !_onGround || _isJumping) return;
        // Add jumping velocity
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y + Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), _rb.velocity.z);
        _isJumping = true;
    }
}
