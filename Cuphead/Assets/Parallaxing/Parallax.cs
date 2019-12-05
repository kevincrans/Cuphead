using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private Transform camera;
    private float speedCoefficient;
    Vector3 lastpos;

    // Start is called before the first frame update
    void Start() {
        camera = GameObject.Find("Main Camera").transform;
        speedCoefficient = this.transform.position.z/100;
        lastpos = camera.position;
    }

    // Update is called once per frame
    void Update() {
        float y = transform.position.y;
        transform.position -= ((lastpos - camera.position) * speedCoefficient);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        lastpos = camera.position;
    }
}
