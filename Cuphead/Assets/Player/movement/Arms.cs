using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Arms : MonoBehaviour {

    [SerializeField] private PlayerShootingHandler playerShooting;

    private InputHandler controls;
    private int lastShotIndex = 0;
    private Vector2 target;

    private void Awake() {
        controls = new InputHandler();

        //TODO change shooting to some form of updating version
        controls.Player.Shoot.started += ctx => StartShooting();
        controls.Player.Aim.performed += ctx => SetTarget(controls.Player.Aim.ReadValue<Vector2>());
    }

    private void SetTarget(Vector2 newTarget) {
        float targetCheck = newTarget.x * newTarget.y;
        if(targetCheck > 1 || targetCheck < -1) {
            //Translate world space to "look at direction"
            Vector3 worldSpacePoint = Camera.main.ScreenToWorldPoint(newTarget);

            float x = (1 / transform.position.x) * worldSpacePoint.x;
            float y = (1 / transform.position.y) * worldSpacePoint.y;

            newTarget.Set(-x, -y);
        }

        target = newTarget;
    }

    private void OnEnable() {
        controls.Player.Enable();
    }

    private void StartShooting() {
        playerShooting.GunShoot(this.transform.parent.GetComponent<CharacterController2D>().m_FacingRight);
        lastShotIndex = 10;
    }

    // Update is called once per frame
    private void Update() {
        Vector3 target = this.target;
        target = this.transform.position + target;
        //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        // /= 60;
        //AngleDeg = Mathf.Floor(AngleDeg);
        //AngleDeg *= 60;

        // Rotate Object
        if(this.transform.parent.GetComponent<CharacterController2D>().m_FacingRight)
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        else
            this.transform.rotation = Quaternion.Euler(0, 0, 180 + AngleDeg);

        //Let the player face the mouse
        if(this.transform.parent.GetComponent<CharacterController2D>().m_FacingRight != (target.x > this.transform.position.x))
            this.transform.parent.GetComponent<CharacterController2D>().Flip();
    }

}
