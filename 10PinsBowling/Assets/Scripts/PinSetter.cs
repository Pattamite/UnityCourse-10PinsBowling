using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public Text standingText;

    private Ball ball;

    public int lastStandingCount = -1;
    public float settleTime = 3f;

    private bool isBallEnter = false;
    private float lastChangeTime;
    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void Update () {
        standingText.text = CountStanding().ToString();

        if (isBallEnter) {
            CheckStanding();
        }
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

    void CheckStanding () {
        int currentStanding = CountStanding();

        if(currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
        }
        else {
            if((Time.time - lastChangeTime) > settleTime) {
                PinHaveSettled();
            }
        }
    }

    void PinHaveSettled () {
        lastStandingCount = -1;
        standingText.color = Color.green;

        isBallEnter = false;
        ball.Reset();
    }

    private void OnTriggerEnter (Collider collider) {
        GameObject gameObject = collider.gameObject;
        if (gameObject.GetComponent<Ball>()) {
            isBallEnter = true;
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
