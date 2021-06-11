//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using System.IO;
using UnityEngine;

namespace MonoBehaviours.Player
{
    public class ScreenShotTaker : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.F2)) return;
            var now = DateTime.Now;
            // 2021-6-8 11.54.30.399
            var fileName = "" + now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + "." + now.Minute + "." +
                           now.Second + "." + now.Millisecond;
            Directory.CreateDirectory(Application.persistentDataPath + "/Screenshots/");
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screenshots/" + fileName + ".png");
            Debug.Log("Screenshot Captured! ");
        }
    }
}