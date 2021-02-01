using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float sensitivity = 6.0f;
    public float distance = 6.25f;

    Vector3 cameraOffset;

    float deltaX = 0f;
    float deltaY = 0f;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        deltaX += Input.GetAxisRaw("Mouse X");
        deltaY = Mathf.Clamp(deltaY - Input.GetAxisRaw("Mouse Y"), -14f, 14f);
    }

    void LateUpdate() {
        Quaternion rotation = Quaternion.Euler(deltaY * sensitivity, deltaX * sensitivity, 0);

        transform.position = player.position + rotation * new Vector3(0, 0, -distance);
        transform.LookAt(player);
    }
}
