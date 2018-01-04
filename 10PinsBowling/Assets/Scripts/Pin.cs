using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingTreshold;
    public float distanceToRaise = 80f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public bool IsStanding () {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        float tiltInX = Mathf.Abs(rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if (tiltInX > 180f) {
            tiltInX = 360f - tiltInX;
        }
        if (tiltInZ > 180f) {
            tiltInZ = 360f - tiltInZ;
        }
       
        if (tiltInX <= standingTreshold && tiltInZ <= standingTreshold) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public void Raise () {
        if (IsStanding()) {
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
        }
    }

    public void Lower () {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }
}
