using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    protected float damage;
    protected BulletType bulletType;
    [SerializeField] protected GameObject bulletPrefab;
    protected float bulletSpeed;

    public void Shoot(bool lookingRight) {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.GetComponent<Bullet>().Init(this, this.bulletType, bulletSpeed, lookingRight);
    }

}
