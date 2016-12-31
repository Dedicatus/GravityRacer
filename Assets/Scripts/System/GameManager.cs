using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager current;
    public GameObject player;

    public int gameScore;
	public int gameHighScore;

    public enum GameState
    {
        Start,
        Running
    }

    public GameState state;

    public void StartGame()
    {
        //GameObject.Find("Player").active = true;
        //player.active = true;
        Player.current.playerState = Player.PlayerState.Playing;
        print("Running");
        state = GameState.Running;
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
		gameHighScore = PlayerPrefs.GetInt ("High Score");
        current = this;
        state = GameState.Start;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
