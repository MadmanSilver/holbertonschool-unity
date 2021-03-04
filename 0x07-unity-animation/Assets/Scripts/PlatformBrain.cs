using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrain : MonoBehaviour
{
    public GameObject[] flowerPrefabs;

    List<GameObject> flowers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject flowerPrefab = flowerPrefabs[Random.Range(0, flowerPrefabs.Length)];
        GameObject flower;

        foreach (Transform child in transform) {
            flower = Instantiate(flowerPrefab, child.position, child.rotation);
            flower.transform.parent = child;
            flower.SetActive(false);
            flowers.Add(flower);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            foreach (GameObject flower in flowers) {
                flower.SetActive(true);
            }
            other.gameObject.transform.parent = transform.parent;
        }
    }
}
