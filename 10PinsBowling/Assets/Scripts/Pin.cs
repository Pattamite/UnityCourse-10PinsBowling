using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingTreshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print(name + " " +  IsStanding());
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
}
