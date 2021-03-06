﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObstacle : Obstacle {

	// Use this for initialization
	void Start () {
        startAnim();	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<MoveToDecSpeedWithoutRot>().reached)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BoxCollider>().enabled = true;
        }
	}

    public void startAnim()
    {
        if (gameObject.GetComponent<MoveToDecSpeedWithoutRot>() == null)
            gameObject.AddComponent<MoveToDecSpeedWithoutRot>();
        MoveToDecSpeedWithoutRot anim = gameObject.GetComponent<MoveToDecSpeedWithoutRot>();
        anim.maxSpeed = 100;
        anim.minSpeed = 50;
        anim.to = transform.position;
        transform.position = transform.position + new Vector3(0, 100, 0);
        anim.from = transform.position;
        anim.resetAnim();
    }
}
