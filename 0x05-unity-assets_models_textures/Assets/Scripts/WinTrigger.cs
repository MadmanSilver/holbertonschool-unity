using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Collider player;
    public Timer timer;
    public Text timerText;

    void OnTriggerEnter(Collider other) {
        if (other == player) {
            timer.Stop();
            timerText.fontSize = 60;
            timerText.color = Color.green;
        }
    }
}
