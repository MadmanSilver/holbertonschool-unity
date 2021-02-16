using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBrain : MonoBehaviour
{
    public GameObject prefab;
    public Transform start;
    public Transform end;
    public PauseMenu pm;

    public float turnSpeed = 20f;

    List<Transform> platforms = new List<Transform>();

    int turning = 0;
    float totalAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = start.position;
        Vector3 position2;
        GameObject platform;

        int mod;

        while (position.z - end.position.z <= -20) {
            mod = Random.Range(-1, 2);
            position = new Vector3(start.position.x + 15 * mod, start.position.y - (15 * Random.Range(0 + Mathf.Abs(mod), 3 - Mathf.Abs(mod))), position.z + 10);
            platform = Instantiate(prefab, position, Quaternion.identity);
            platforms.Add(platform.transform);
            platform.transform.parent = start.parent;

            if (Random.Range(0, 2) == 1) {
                do {
                    mod = Random.Range(-1, 2);
                    position2 = new Vector3(start.position.x + 15 * mod, start.position.y - (15 * Random.Range(0 + Mathf.Abs(mod), 3 - Mathf.Abs(mod))), position.z);
                } while (position2 == position);
                platform = Instantiate(prefab, position2, Quaternion.identity);
                platforms.Add(platform.transform);
                platform.transform.parent = start.parent;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!pm.paused) {
            if (Input.GetKeyDown(KeyCode.E)) {
                turning++;
            }
        }
    }

    void FixedUpdate()
    {
        if (turning > 0) {
            float rotation;

            if (totalAngle + turnSpeed * Time.deltaTime > 90) {
                rotation = 90 - totalAngle;
                totalAngle = 0f;
                turning--;
            } else {
                rotation = turnSpeed * Time.deltaTime;
                totalAngle += rotation;
            }

            foreach (Transform platform in platforms) {
                /*platform.RotateAround(new Vector3(start.position.x, start.position.y - 15, start.position.z), Vector3.forward, rotation);
                platform.rotation = Quaternion.Euler(0, 0, 0);*/
                Vector3 origin = new Vector3(start.position.x, start.position.y - 15, start.position.z);
                platform.position = origin + Quaternion.Euler(0, 0, rotation) * (platform.position - origin);
            }
        }
    }
}
