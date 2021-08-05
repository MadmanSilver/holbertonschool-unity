using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text winText;

    float totalTime = 0f;
    bool running = true;

    // Update is called once per frame
    void Update()
    {
        if (running) {
            totalTime += Time.deltaTime;
            timerText.text = $"{(int)totalTime / 60}:{(totalTime % 60).ToString("00.00")}";
        }
    }

    public void Stop() {
        running = false;
    }

    public void Win() {
        winText.text = timerText.text;
        timerText.text = "";
    }
}
