using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustceneController : MonoBehaviour
{
    public GameObject mainCam;
    public PlayerController playerControl;
    public GameObject timer;
    public GameObject tutorial;
    public Animator anim;

    void Start() {
        anim.SetInteger("Level", SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnControl() {
        mainCam.SetActive(true);
        playerControl.enabled = true;
        timer.SetActive(true);
        if (tutorial != null)
            tutorial.SetActive(true);
        gameObject.SetActive(false);
    }
}
