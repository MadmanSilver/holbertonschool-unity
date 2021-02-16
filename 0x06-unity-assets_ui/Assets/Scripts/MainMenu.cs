using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int level) {
        SceneManager.LoadScene("Level" + level.ToString("00"));
    }

    public void Options() {
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Options");
    }

    public void Exit() {
        Debug.Log("Exited");
        Application.Quit();
    }
}
