using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour { 
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Vector3 startPosition;
    
	// Use this for initialization
	void Start(){
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Launch(Vector3 velocity) {
        inPlay = true;

        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void Reset () {
        transform.position = startPosition;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
