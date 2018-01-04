using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Ball ball;
    public float cameraLimitZ = 1800;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ball.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (ball.transform.position.z <= cameraLimitZ) {
            transform.position = ball.transform.position + offset;
        }
        

    }
}
