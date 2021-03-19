using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle inverted;
    public Slider bgm;
    public Slider sfx;
    public AudioMixer master;

    void Start() {
        if (PlayerPrefs.GetInt("Inverted") == 1) {
            inverted.isOn = true;
        } else {
            inverted.isOn = false;
        }

        bgm.value = PlayerPrefs.GetFloat("BGMVolume");
        sfx.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void Back() {
        SetBGMVol(PlayerPrefs.GetFloat("BGMVolume"));
        SetSFXVol(PlayerPrefs.GetFloat("SFXVolume"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
    }

    public void Apply() {
        if (inverted.isOn) {
            PlayerPrefs.SetInt("Inverted", 1);
        } else {
            PlayerPrefs.SetInt("Inverted", 0);
        }

        PlayerPrefs.SetFloat("BGMVolume", bgm.value);
        PlayerPrefs.SetFloat("SFXVolume", sfx.value);

        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
    }

    public void SetBGMVol(float vol) {
        master.SetFloat("bgmVol", LinearToDecibel(vol));
    }

    public void SetSFXVol(float vol) {
        master.SetFloat("sfxVol", LinearToDecibel(vol));
    }

    private float LinearToDecibel(float linear) {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }
}
