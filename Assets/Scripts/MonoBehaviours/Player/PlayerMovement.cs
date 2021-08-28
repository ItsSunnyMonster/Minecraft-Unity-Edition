// 
// Copyright (c) SunnyMonster
//

using System;
using UnityEngine;

[RequireComponent(typeof(RigidbodyCharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Options")]
    public float speed = 40;

    private RigidbodyCharacterController _controller;
    private float _horizontalMove;
    private float _verticalMove;

    private void Start()
    {
        _controller = GetComponent<RigidbodyCharacterController>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        _verticalMove   = Input.GetAxisRaw("Vertical") * speed;
    }

    private void FixedUpdate()
    {
        var move = transform.right * _horizontalMove + transform.forward * _verticalMove;
        _controller.Move(move.x * Time.fixedDeltaTime, move.z * Time.fixedDeltaTime, Input.GetButton("Jump"));
    }
}
