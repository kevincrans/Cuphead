using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGun : Gun {

    // Just init
    void Start() {
        this.damage = 1;
        this.bulletType = BulletType.BLUE;
        this.bulletSpeed = 24;
    }
}
