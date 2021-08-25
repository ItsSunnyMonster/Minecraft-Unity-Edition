//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections.Generic;
using TMPro;
using UnityEngine;  
using Random = System.Random;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI flashingText;

    public List<string> flashTexts = new List<string>();

    private void Start()
    {
        var random = new Random();
        if (flashTexts.Count > 0)
        {
            // Set random text
            flashingText.text = flashTexts[random.Next(0, flashTexts.Count)];
        }
    }
}
