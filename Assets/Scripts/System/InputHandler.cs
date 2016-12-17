using UnityEngine;
using System.Collections;


public class InputHandler : MonoBehaviour {
    
	void Awake () {
	
	}
	
    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
        if (Player.current == null) return;
        if (Input.GetKey(KeyCode.A))
        {
            Player.current.RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Player.current.RotateRight();
        }
    }
}
