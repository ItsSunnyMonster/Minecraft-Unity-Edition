//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private Vector3 _yVelocity;

    public float speed = 4.317f;
    public float sprintSpeed = 5.612f;
    public float gravity = -31.36f;
    public float jumpHeight = 1.25f;

    private bool _isGrounded;

    private void Update()
    {
        if (_isGrounded && _yVelocity.y < 0)
        {
            _yVelocity.y = -2f;
        }

        // Get input
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        // Move the player based on input
        var move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // Check for jumping input and jump if button down
        if (Input.GetButton("Jump") && _isGrounded)
        {
            _yVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Move the player based on gravity
        _yVelocity.y += gravity * Time.deltaTime;
        controller.Move(_yVelocity * Time.deltaTime);
        
        _isGrounded = controller.isGrounded;
    }
}