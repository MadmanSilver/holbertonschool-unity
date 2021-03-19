using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer timer;

    void OnTriggerExit(Collider other) {
        if (other.gameObject == timer.gameObject) {
            timer.enabled = true;
        }
    }
}
