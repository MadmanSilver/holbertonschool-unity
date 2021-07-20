using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public Text debugText;
    public LineRenderer line;
    public float reelForce = 1f;
    private Vector3 castPoint = Vector3.zero;
    private RaycastHit hit;
    private Vector3 lastTipPosition;

    // Update is called once per frame
    void Update() {
        line.SetPositions(new Vector3[] {transform.TransformPoint(new Vector3(0f, 0.48f, 0f)), castPoint});

        debugText.text = $"Point1 - x: {line.GetPosition(0).x} y: {line.GetPosition(0).y} z: {line.GetPosition(0).z}\nPoint2 - x: {line.GetPosition(1).x} y: {line.GetPosition(1).y} z: {line.GetPosition(1).z}";
    }

    void LateUpdate() {
        if (castPoint == Vector3.zero) {
            return;
        }

        if ((lastTipPosition - line.GetPosition(1)).magnitude > (line.GetPosition(0) - line.GetPosition(1)).magnitude + reelForce) {
            Reel();
        }

        lastTipPosition = line.GetPosition(0);
    }

    public void Cast() {
        if (castPoint == Vector3.zero) {
            return;
        }

        if (Physics.Raycast(line.GetPosition(0), Vector3.down, out hit, 10f)) {
            line.positionCount = 2;
            castPoint = hit.point;
            line.SetPosition(1, castPoint);
        }
    }

    public void Reel() {
        if (castPoint != Vector3.zero) {
            return;
        }
        
        castPoint = Vector3.zero;
        line.positionCount = 1;
    }
}
