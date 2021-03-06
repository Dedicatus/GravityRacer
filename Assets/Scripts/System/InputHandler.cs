﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class InputHandler : MonoBehaviour {
	bool flag = false;
    
	void Awake () {
	
	}
	
    void Start()
    {
    }
	void AccelarePlayer(){
        //Player.current.GetComponentInChildren<SelfRotateByZ>().rotateRate = 120;
		//Player.current.force = Player.current.accelerateForce;
        Player.current.Accelerate();
	}
	void RecoverPlayer(){
        ////Player.current.GetComponentInChildren<SelfRotateByZ> ().rotateRate = 60;
        //Player.current.force = Player.current.pushForce;
        Player.current.Recover();
	}

	void FixedUpdate ()
    {
        if (GameManager.current.state == GameManager.GameState.Start)
        {
            if (Input.touchCount == 1 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                GameManager.current.StartGame();
            }
        }

        if (Player.current == null) return;
        if (Input.GetKey(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
        if(GameManager.current.state == GameManager.GameState.Running)
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                AccelarePlayer();
                flag = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Player.current.RotateLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Player.current.RotateRight();
            }


            if (Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];
                if (touch.position.x < Screen.width / 2.0f)
                {
                    Player.current.RotateLeft();
                }
                else
                {
                    Player.current.RotateRight();
                }
            }
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];
                if ((touch1.position.x <= Screen.width / 2.0f && touch2.position.x >= Screen.width / 2.0f) || (touch1.position.x >= Screen.width / 2.0f && touch2.position.x <= Screen.width / 2.0f))
                {
                    AccelarePlayer();
                    flag = true;

                }

            }
            if (flag != true)
            {
                RecoverPlayer();

            }
            flag = false;
        } 
        
    }
}
