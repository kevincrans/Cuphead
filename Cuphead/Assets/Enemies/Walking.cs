using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {
    [SerializeField] private float speed;
    private bool walkRight = true;

    // Update is called once per frame
    void Update() {
        if(walkRight)
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(speed * Time.deltaTime, 0), speed * Time.deltaTime);
        else
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - new Vector3(speed * Time.deltaTime, 0), speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.up, -Vector2.up, 5, 1 << LayerMask.NameToLayer("Default"));

        if(hit.collider == null) {
            this.GetComponent<CharacterController2D>().Flip();
            walkRight = !walkRight;
        }
    }
}
