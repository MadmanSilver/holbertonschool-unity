using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle inverted;

    void Start() {
        if (PlayerPrefs.GetInt("Inverted") == 1) {
            inverted.isOn = true;
        } else {
            inverted.isOn = false;
        }
    }

    public void Back() {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
    }

    public void Apply() {
        if (inverted.isOn) {
            PlayerPrefs.SetInt("Inverted", 1);
        } else {
            PlayerPrefs.SetInt("Inverted", 0);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
    }
}
