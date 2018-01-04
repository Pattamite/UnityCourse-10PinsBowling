using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
