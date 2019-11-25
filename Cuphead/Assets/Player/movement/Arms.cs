using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        if(this.transform.parent.GetComponent<CharacterController2D>().m_FacingRight)
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        else
            this.transform.rotation = Quaternion.Euler(0, 0, 180 + AngleDeg);

        //Let the player face the mouse
        if(this.transform.parent.GetComponent<CharacterController2D>().m_FacingRight != (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > this.transform.position.x))
            this.transform.parent.GetComponent<CharacterController2D>().Flip();
    }
}
