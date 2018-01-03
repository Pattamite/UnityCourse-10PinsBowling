using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingText;
    private bool isBallEnter = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        standingText.text = CountStanding().ToString();

    }

    int CountStanding () {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standing++;
            }
        }

        return standing;
    }

    private void OnTriggerEnter (Collider collider) {
        GameObject gameObject = collider.gameObject;
        if (gameObject.GetComponent<Ball>()) {
            isBallEnter = true;
            print("Ball Entered");
            standingText.color = Color.red;
        }
    }

    private void OnTriggerExit (Collider collider) {
        GameObject gameObject = collider.gameObject;
        if (gameObject.GetComponentInParent<Pin>()) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
