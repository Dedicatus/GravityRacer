using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public Text raceScore;
	public Text highScore;
	public Text coinNumbers;
	public Button restart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		raceScore.text = GameManager.current.gameScore.ToString();
		highScore.text = "High Score:" + GameManager.current.gameHighScore.ToString ();
		coinNumbers.text = "Coin:" + GameManager.current.coinCount.ToString ();
		if (Player.current.playerState == Player.PlayerState.Dead) {
			restart.interactable = true;
			//	restart.onClick.AddListener (ReloadGame);
			restart.gameObject.SetActive(true);

		} else {
			restart.interactable = false;
			restart.gameObject.SetActive (false);
		}
		  
	}
	void ReloadGame(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

}

