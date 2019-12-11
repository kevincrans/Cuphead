using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour {
    [SerializeField] private Gun gun;

    public void GunShoot(bool lookingRight) {
        gun.Shoot(lookingRight);
    }
}
