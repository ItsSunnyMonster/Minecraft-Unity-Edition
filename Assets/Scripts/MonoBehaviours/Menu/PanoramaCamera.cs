//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

namespace MonoBehaviours.Menu
{
    public class PanoramaCamera : MonoBehaviour
    {
        public float rotateSpeed = 5f;

        public void FixedUpdate()
        {
            transform.Rotate(0, rotateSpeed * Time.fixedDeltaTime, 0);
        }
    }
}