//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 _yVelocity;

    public float speed = 12f;
    public float groundDistance = 0.4f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    private bool _isGrounded;

    private void Update()
    {
        // Do ground check
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _yVelocity.y < 0)
        {
            _yVelocity.y = -2f;
        }

        // Get input
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        // Move the player based on input
        var move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));

        // Check for jumping input and jump if button down
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _yVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Move the player based on gravity
        _yVelocity.y += gravity * Time.deltaTime;
        controller.Move(_yVelocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw ground check gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}