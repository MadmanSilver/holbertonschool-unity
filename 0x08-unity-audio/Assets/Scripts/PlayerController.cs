using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public PauseMenu pm;
    public AudioSource footsteps;
    public AudioSource landing;

    public Vector3 respawn = new Vector3(0, 0, 0);
    
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float killFloor = -40;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVeloctity;

    Vector3 velocity;
    public bool isGrounded;
    public bool isWalking;
    public float falling;
    public bool canMove = true;

    public GameObject tutorial;
    bool tutMove = false;
    bool tutLook = false;
    bool tutJump = false;
    bool tutRotate = false;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pm.paused) {
                pm.Resume();
            } else {
                pm.Pause();
            }
        }

        if (!pm.paused) {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore);
            anim.SetBool("isGrounded", isGrounded);

            if (isGrounded && velocity.y < -5 && anim.GetBool("respawned")) {
                landing.Play();
            }

            if (isGrounded && velocity.y < 0) {
                velocity.y = -2f;
            } else if (!isGrounded && velocity.y < 0) {
                falling += Time.deltaTime;
            }

            if (isGrounded) {
                falling = 0f;
            }

            if (canMove) {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f) {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVeloctity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                    isWalking = true;

                    if (isGrounded && !footsteps.isPlaying) {
                        footsteps.Play();
                    }
                } else {
                    isWalking = false;
                }
                anim.SetBool("isWalking", isWalking);

                if (Input.GetButtonDown("Jump") && isGrounded) {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    transform.parent = null;
                    tutJump = true;
                    if (tutorial != null)
                        tutorial.transform.GetChild(3).GetComponent<Text>().color = Color.green;
                    anim.SetTrigger("jump");
                }

                if (tutorial != null) {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) {
                        tutMove = true;
                        tutorial.transform.GetChild(1).GetComponent<Text>().color = Color.green;
                    }

                    if (Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0.1f) {
                        tutLook = true;
                        tutorial.transform.GetChild(2).GetComponent<Text>().color = Color.green;
                    }

                    if (Input.GetKeyDown(KeyCode.E)) {
                        tutRotate = true;
                        tutorial.transform.GetChild(4).GetComponent<Text>().color = Color.green;
                    }

                    if (tutMove && tutJump && tutLook && tutRotate) {
                        tutorial.SetActive(false);
                    }
                }
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (transform.position.y < killFloor) {
                transform.position = respawn;
                transform.parent = null;
                canMove = false;
                anim.SetBool("respawned", true);
            }
        }
    }
}
