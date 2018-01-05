using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();
    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;
	
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        ball = GameObject.FindObjectOfType<Ball>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Bowl (int pinFall) {
        bowls.Add(pinFall);
        ball.Reset();
        pinSetter.PreformAction(ActionMaster.NextAction(bowls));
        scoreDisplay.FillRolls(bowls);
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(bowls));

    }
}
