using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public Text DisplayText;
    public int Score;
    private int OldScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Score != OldScore)
        {
            DisplayText.text = "Score: " + Score;
            OldScore = Score;
        }
	}
}
