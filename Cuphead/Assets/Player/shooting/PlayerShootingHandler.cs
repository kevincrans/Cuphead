using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour {
    [SerializeField] private Gun gun;

    public void GunShoot() {
        gun.Shoot();
    }
}
