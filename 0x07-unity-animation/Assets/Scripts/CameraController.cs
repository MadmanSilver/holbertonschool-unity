using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public PauseMenu pm;

    public bool isInverted;
    public float sensitivity = 6.0f;
    public float distance = 6.25f;

    Vector3 cameraOffset;

    float deltaX = 0f;
    float deltaY = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        if (PlayerPrefs.GetInt("Inverted") == 1) {
            isInverted = true;
        } else {
            isInverted = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pm.paused) {
            deltaX += Input.GetAxisRaw("Mouse X");
            if (isInverted) {
                deltaY = Mathf.Clamp(deltaY + Input.GetAxisRaw("Mouse Y"), -14f, 14f);
            } else {
                deltaY = Mathf.Clamp(deltaY - Input.GetAxisRaw("Mouse Y"), -14f, 14f);
            }
        }
    }

    void LateUpdate() {
        if (!pm.paused) {
            Quaternion rotation = Quaternion.Euler(deltaY * sensitivity, deltaX * sensitivity, 0);

            transform.position = player.position + rotation * new Vector3(0, 0, -distance);
            transform.LookAt(player);
        }
    }
}
