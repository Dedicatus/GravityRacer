﻿using UnityEngine;
using System.Collections;

public class SelfRotateByZ : MonoBehaviour {

	public float rotateRate;

	void Start () {
	
	}
	
	void Update () {
		transform.Rotate(Vector3.forward, rotateRate * Time.deltaTime);
	}
}
