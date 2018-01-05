using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();
    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;
    private int highScore;
    private string highScoreKey = "HIGH_SCORE";

    void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        ball = GameObject.FindObjectOfType<Ball>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();

        if (PlayerPrefs.HasKey(highScoreKey)) {
            highScore = PlayerPrefs.GetInt(highScoreKey);
        }
        else {
            PlayerPrefs.SetInt(highScoreKey, 0);
            highScore = 0;
        }

        scoreDisplay.SetHighScore(highScore);
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

    public void Reset () {
        int currentScore = ScoreMaster.ScoreTotal(bowls);
        if(currentScore > highScore) {
            highScore = currentScore;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            scoreDisplay.SetHighScore(highScore);
        }
        scoreDisplay.Reset();
        bowls = new List<int>();
    }
}
