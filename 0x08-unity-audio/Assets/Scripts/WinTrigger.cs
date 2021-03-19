using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Collider player;
    public Timer timer;
    public Text timerText;
    public GameObject winCanvas;
    public PauseMenu pm;
    public AudioSource bgm;
    public AudioSource sting;

    void OnTriggerEnter(Collider other) {
        if (other == player) {
            timer.Stop();
            timer.Win();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pm.paused = true;
            winCanvas.SetActive(true);
            bgm.Stop();
            sting.Play();
        }
    }
}
