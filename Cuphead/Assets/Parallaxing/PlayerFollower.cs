using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    // Update is called once per frame
    void Update() {
        this.transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }
}
