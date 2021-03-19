using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot unpausedSnap;
    public AudioMixerSnapshot pausedSnap;

    public bool paused = false;

    public void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        paused = true;
        gameObject.SetActive(true);
        pausedSnap.TransitionTo(0f);
    }

    public void Resume() {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        gameObject.SetActive(false);
        unpausedSnap.TransitionTo(0f);
    }

    public void Restart() {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        unpausedSnap.TransitionTo(0f);
    }

    public void MainMenu() {
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("MainMenu");
        unpausedSnap.TransitionTo(0f);
    }

    public void Options() {
        Time.timeScale = 1;
        paused = false;
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Options");
        unpausedSnap.TransitionTo(0f);
    }
}
