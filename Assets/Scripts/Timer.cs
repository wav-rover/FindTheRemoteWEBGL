using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image timerImage;
    float elapsedTime;

    void Update()
    {
        bool showTimer = PlayerPrefs.GetInt("ShowTimer", 1) == 1;

        if (showTimer)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss");
            timerText.enabled = true;
            timerImage.enabled = true;
        }
        else
        {
            timerText.enabled = false;
            timerImage.enabled = false;
        }
    }
}