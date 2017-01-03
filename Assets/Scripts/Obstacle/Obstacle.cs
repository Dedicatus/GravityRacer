using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public int index;
	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.other.tag == "Player")
        {
            Player.current.GetComponent<Rigidbody>().constraints = 0;
            Player.current.Die();

        }
    }
}
