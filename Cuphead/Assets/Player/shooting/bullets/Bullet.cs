using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Gun gun;
    private BulletType bulletType;
    private float speed;
    private bool lookingRight;

    public void Init(Gun gun, BulletType bulletType, float speed, bool lookingRight) {
        this.gun = gun;
        this.bulletType = bulletType;
        this.speed = speed;
        this.lookingRight = lookingRight;
    }

    private void Update() {
        if(this.lookingRight)
            this.transform.position += this.transform.right * speed * Time.deltaTime;
        else
            this.transform.position -= this.transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.layer.CompareTo(gameObject.layer) != 0)
            Destroy(gameObject);
    }

}
