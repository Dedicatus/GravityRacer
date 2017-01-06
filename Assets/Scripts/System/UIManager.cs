using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public Text raceScore;
	public Text highScore;
	public Text coinNumbers;
    public Text FPS;
	public Button restart;

    float deltaTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		raceScore.text = GameManager.current.gameScore.ToString();
		highScore.text = "High Score:" + GameManager.current.gameHighScore.ToString ();
		coinNumbers.text = "Coin:" + GameManager.current.coinCount.ToString ();

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        FPS.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

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

