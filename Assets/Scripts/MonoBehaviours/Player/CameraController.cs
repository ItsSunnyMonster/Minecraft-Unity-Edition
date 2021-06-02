//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

namespace MonoBehaviours.Player
{
    public class CameraController : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        public float speed = 10f;
        public float increasedSpeed = 20f;

        private float _xRotation = 0f;
        private float _yRotation = 0f; // Keeping track of the rotation so that we can clamp it afterwards

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        }

        private void Update()
        {
            // Get mouse position input
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            // Set the rotation and clamp it
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            _yRotation += mouseX;

            // Rotate according to the input
            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        
            // Move the camera
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                transform.Translate(new Vector3(horizontal, 0f, vertical) * (Time.deltaTime * increasedSpeed));
            }
            else
            {
                transform.Translate(new Vector3(horizontal, 0f, vertical) * (Time.deltaTime * speed));
            }
        }
    }
}