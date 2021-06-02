//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using UnityEngine;

namespace MonoBehaviours.Player
{
    public class SpectatorControl : MonoBehaviour
    {
        public float speed;
        public float scrollWheelSensitivity;

        private Vector3 _input;
        private float _scrollInput;

        private void OnEnable()
        {
            GetComponent<CharacterController>().enabled = false;
        }

        private void OnDisable()
        {
            GetComponent<CharacterController>().enabled = true;
        }

        private void Update()
        {
            // Get inputs
            _input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Up and Down"), Input.GetAxis("Vertical"));
            _scrollInput = Input.GetAxis("Mouse ScrollWheel");
        }

        private void FixedUpdate()
        {
            // Move the player and set the speed based on scroll input
            transform.Translate(_input * (speed * Time.fixedDeltaTime));
            speed += _scrollInput * scrollWheelSensitivity * Time.fixedDeltaTime;
        }
    }
}