using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 500f;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winloseText;
    public Image winloseBG;

    private int score = 0;

    // Update is called once per frame
    void Update() {
        if (health == 0) {
            winloseText.text = "Game Over!";
            winloseText.color = Color.white;
            winloseBG.color = Color.red;
            winloseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("menu");
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("d")) {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey("a")) {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey("w")) {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
        
        if (Input.GetKey("s")) {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Pickup") {
            score++;
            SetScoreText();
            Object.Destroy(other.gameObject);
        }

        if (other.tag == "Trap") {
            health--;
            SetHealthText();
        }

        if (other.tag == "Goal") {
            winloseText.text = "You Win!";
            winloseText.color = Color.black;
            winloseBG.color = Color.green;
            winloseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText() {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText() {
        healthText.text = $"Health: {health}";
    }

    IEnumerator LoadScene(float seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
