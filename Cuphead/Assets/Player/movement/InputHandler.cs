using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    //Implement the character controller
    public CharacterController2D controller;
    AudioSource audioData;

    //Player speed
    public float normalSpeed = 5f;
    public float runningSpeed = 7f;

    //Variables to update our character in the fixed update instead of the update function
    float horizontalMove = 0f;
    [Header("Puike booleans")]
    public bool jumping = false;
    public bool inAir = false;
    public bool walking = false;
    public bool running = false;
    public bool crouching = false;
    public bool pushing = false;
    private GameObject pushedObject;
    private int jumpCooldown = 0;
    private bool dieing = false;

    //Implement the animator for player animations
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        loadAudio();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftControl)) { crouching = true; } else { crouching = false; }

        if (!crouching) crouching = controller.getCrouching();
        if (pushing) {
            if (pushedObject.GetComponent<Rigidbody2D>().mass > 10) pushing = false; else crouching = true;
        } else {
            if (pushedObject != null)
                if (Vector3.Distance(pushedObject.transform.position, transform.position) <= 9.82f) {
                    pushing = true;
                }
        }

        float speed = normalSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !crouching) speed = runningSpeed;
        if (pushing) {
            speed = normalSpeed * 1.8f;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (horizontalMove != 0) {
            if (speed < runningSpeed) { walking = true; running = false; } else { running = true; walking = true; }
        } else {
            running = false;
            walking = false;
        }

        if (Input.GetButton("Jump") && jumpCooldown >= 10 && (!crouching || pushing)) {
            jumping = true;
            jumpCooldown = 0;
        }
    }

    //If player is pushing a "moveable"
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Moveable") {
            pushing = true;
            pushedObject = col.gameObject;
        }
    }

    //Move the player via the charactercontroller
    void FixedUpdate() {

        if (dieing) {
            controller.Move(0, false, false);
            return;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouching, jumping);

        if (!controller.m_Grounded) inAir = true; else inAir = false;

        //animator.SetBool("walking", walking);
        //animator.SetBool("running", running);
        //animator.SetBool("jumping", inAir);
        //animator.SetBool("crouching", crouching);

        jumping = false;

        if (controller.m_Grounded && jumpCooldown < 10) jumpCooldown++;
    }

    public void Die(GameObject enemy, bool facingRight) {
        controller.Move(0, false, false);
        dieing = true;

        controller.m_FacingRight = facingRight;
        animator.SetBool("dieing", true);
    }

    public void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void disableAnimatorDie() {
        animator.SetBool("dieing", false);
    }

    //Audio

    private void loadAudio() {
        audioData = GetComponent<AudioSource>();
    }

    public void WalkAudio() {
        AudioClip clip = (AudioClip)Resources.Load("Audio/Player/Concrete_Boots_Walking");
        audioData.PlayOneShot(clip);
    }

    public void RunAudio() {
        AudioClip clip = (AudioClip)Resources.Load("Audio/Player/Concrete_Boots_Running");
        audioData.PlayOneShot(clip);
    }

    public void BoxAudio() {
        if (!pushing) return;
        AudioClip clip = (AudioClip)Resources.Load("Audio/Box/Box");
        audioData.PlayOneShot(clip);
    }

    public void stopAudio() {
        audioData.Stop();
    }
}
