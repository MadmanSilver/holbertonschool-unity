using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public Text debugText;
    public int biteChance;
    public float biteWindow = 2f;
    public LineRenderer line;
    public float reelForce = 1f;
    public GameObject lure;
    public GameObject fishPrefab;
    private Vector3 castPoint = Vector3.zero;
    private RaycastHit hit;
    private Vector3 lastTipPosition;
    private bool fishing = false;
    private float biteTime = 0f;
    private GameObject caught = null;
    private float fishingTime = 0f;

    // Update is called once per frame
    void Update() {
        if (caught != null) {
            castPoint = line.GetPosition(0) - new Vector3(0f, 0.3f, 0f);
            caught.transform.position = castPoint;
        }

        line.SetPositions(new Vector3[] {transform.TransformPoint(new Vector3(0f, 0.48f, 0f)), castPoint});

        if (fishing) {
            if ((fishingTime + Time.deltaTime) / 1f > fishingTime / 1f && Random.Range(0, biteChance) == 0) {
                Bite();
            }

            fishingTime += Time.deltaTime;

            if (biteTime > 0f) {
                biteTime -= Time.deltaTime;
            }
        }

        debugText.text = $"Point1 - x: {line.GetPosition(0).x / 0.1f} y: {line.GetPosition(0).y / 0.1f} z: {line.GetPosition(0).z / 0.1f}\nPoint2 - x: {line.GetPosition(1).x / 0.1f} y: {line.GetPosition(1).y / 0.1f} z: {line.GetPosition(1).z / 0.1f}";
    }

    void LateUpdate() {
        if (castPoint == Vector3.zero) {
            return;
        }

        if ((lastTipPosition - line.GetPosition(1)).magnitude > (line.GetPosition(0) - line.GetPosition(1)).magnitude + reelForce && caught == null) {
            Reel();
        }

        lastTipPosition = line.GetPosition(0);
    }

    public void Cast() {
        if (castPoint != Vector3.zero) {
            return;
        }

        if (Physics.Raycast(line.GetPosition(0), Vector3.down, out hit, 10f)) {
            line.positionCount = 2;
            castPoint = hit.point;
            line.SetPosition(1, castPoint);
            lure.SetActive(true);
            lure.transform.position = castPoint;

            if (hit.collider.gameObject.layer == 4) {
                fishing = true;
            }
        }
    }

    public void Reel() {
        if (castPoint == Vector3.zero) {
            return;
        }

        lure.SetActive(false);
        if (biteTime > 0f) {
            Catch();
        } else {
            castPoint = Vector3.zero;
            line.positionCount = 1;
            fishing = false;
            caught = null;
        }
    }

    private void Bite() {
        biteTime = biteWindow;
        castPoint = castPoint - new Vector3(0f, 0.1f, 0f);
        lure.transform.position = castPoint;
        line.SetPosition(1, castPoint);
    }

    private void Catch() {
        biteTime = 0f;
        castPoint = line.GetPosition(0) - new Vector3(0f, 0.3f, 0f);
        line.SetPosition(1, castPoint);
        caught = Object.Instantiate(fishPrefab, castPoint, Quaternion.identity);
        caught.transform.parent = transform;
        fishing = false;
    }
}
