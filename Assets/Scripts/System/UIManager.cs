using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public Text raceScore;
	public Text highScore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		raceScore.text = GameManager.current.gameScore.ToString();
		highScore.text = "High Score:" + GameManager.current.gameHighScore.ToString ();
	}
}
