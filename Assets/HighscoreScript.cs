using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour {

    public string CurrentScorePrefix = "";
    public Text CurrentScore;

    public string HighScorePrefix = "";
    public Text HighScore;

	// Use this for initialization
	void Start () {
        int previousScore = PlayerPrefs.GetInt("PlayScore");
        int highestScore = PlayerPrefs.GetInt("HighScore");

        if (previousScore > highestScore)
        {
            highestScore = previousScore;
            PlayerPrefs.SetInt("HighScore", highestScore);
        }

        CurrentScore.text = CurrentScorePrefix + previousScore.ToString();
        HighScore.text = HighScorePrefix + highestScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
