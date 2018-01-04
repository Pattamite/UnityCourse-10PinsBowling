using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public int lastStandingCount = -1;
    public float settleTime = 3f;
    public Text standingText;
    public GameObject pinSet;

    private bool isBallEnter = false;
    private float lastChangeTime;
    private Ball ball;
    private int lastSettledCount = 10;
    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        standingText.text = CountStanding().ToString();

        if (isBallEnter) {
            CheckStanding();
        }
    }

    public void RaisePins () {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Raise();
        }
    }

    public void LowerPins () {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    public void RenewPins () {
        Instantiate(pinSet, new Vector3(0, 0, 1829), Quaternion.identity);
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

    private void PinHaveSettled () {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionAfterBowl(pinFall);

        lastStandingCount = -1;
        standingText.color = Color.green;

        isBallEnter = false;
        ball.Reset();
    }

    private void ActionAfterBowl (int pinFall) {
        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame) {
        }
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
