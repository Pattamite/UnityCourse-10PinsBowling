using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
    public Text standingText;
    public float settleTime = 3f;

    private int lastStandingCount = -1;
    private bool isBallOutOfPlay = false;
    private float lastChangeTime;
    private int lastSettledCount = 10;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        standingText.text = CountStanding().ToString();

        if (isBallOutOfPlay) {
            CheckStanding();
        }
    }
    

    public int CountStanding () {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standing++;
            }
        }
        return standing;
    }

    private void CheckStanding () {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
        }
        else {
            if ((Time.time - lastChangeTime) > settleTime) {
                PinHaveSettled();
            }
        }
    }

    private void PinHaveSettled () {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);
        lastStandingCount = -1;
        standingText.color = Color.black;

        isBallOutOfPlay = false;
    }

    public void Reset () {
        lastSettledCount = 10;
    }

    private void BallOutOfPlay () {
        isBallOutOfPlay = true;
        standingText.color = Color.red;
    }

    private void OnTriggerExit (Collider collider) {
        if (collider.gameObject.GetComponent<Ball>()) {
            BallOutOfPlay();
        }
    }
}
