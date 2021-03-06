﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager current;
    public GameObject player;

    public int gameScore;
	public int gameHighScore;
    public int coinCount;
    
    public enum GameState
    {
        Start,
        AssembleTrack,
        CutScene,
        Running
    }

    public GameState state;

    public void StartGame()
    {
        Player.current.Launch();
        //Player.current.playerState = Player.PlayerState.Playing;
        print("Running");
        state = GameState.Running;
        Time.timeScale = 1.0f;
        ChallengeManager.current.startTime = Time.time;
        ChallengeManager.current.getHardTimeRemain = 15.0f;
    }

    public void StartCutScene()
    {
        state = GameState.CutScene;
    }

	public void SetHighScore(){
		if (gameScore > gameHighScore) {
			gameHighScore = gameScore;
			PlayerPrefs.SetInt ("High Score",gameHighScore);
		}
	}

    public void ReloadAfterDelay(float delay)
    {
        StartCoroutine(ReloadAfterDelayIE(delay));
    }

    IEnumerator ReloadAfterDelayIE(float delay)
    {
        yield return new WaitForSeconds(delay);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


    void Start () {
        Application.targetFrameRate = 60;
        //QualitySettings.antiAliasing = 0;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowDistance = 150;
        gameHighScore = PlayerPrefs.GetInt ("High Score");
        current = this;
        state = GameState.Start;
        coinCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
