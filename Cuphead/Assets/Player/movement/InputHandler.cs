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
    public float dashSpeed = 10f;

    //Variables to update our character in the fixed update instead of the update function
    float horizontalMove = 0f;
    [Header("Puike booleans")]
    public bool jumping = false;
    public bool inAir = false;
    public bool walking = false;
    public bool dashing = false;
    public bool crouching = false;
    public bool pushing = false;
    [SerializeField] private int dashBoostIndex = 10;
    private int jumpCooldown = 0;
    private bool dieing = false;

    private int dashedIndex = 0;

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

        if (!crouching) 
            crouching = controller.getCrouching();

        if(Input.GetButton("Jump") && jumpCooldown >= 10) {
            jumping = true;
            jumpCooldown = 0;
            dashedIndex = 0;
        }

        float speed = normalSpeed;
        if(Input.GetKey(KeyCode.LeftShift) && !controller.m_Grounded && dashedIndex < dashBoostIndex) {
            speed = dashSpeed;
            dashedIndex++;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (horizontalMove == 0) {
            walking = false;
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
