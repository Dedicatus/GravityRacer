﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow current;

    public GameObject follow;
    public float cameraY;

    public bool zoom;
    public float zoomVelocityLowThreshold;
    public float zoomOutWeight;

	void Start () {
        current = this;

    }
	
	void Update () {
	    if(follow != null)
        {
            Vector3 pos = follow.transform.position;
            if (zoom)
                pos.y = getZoomCameraY(); //cameraY;
            else
                pos.y = pos.y + cameraY;
            transform.position = pos;
        }
	}

    public float getZoomCameraY()
    {
        if (Player.current == null) return 0;
        Vector3 velocity = Player.current.GetComponent<Rigidbody>().velocity;
        Vector3 pos = transform.position;
        print(velocity.magnitude);
        if (velocity.magnitude < zoomVelocityLowThreshold)
        {
            pos.y = cameraY;
        }
        else
        {
            pos.y = cameraY + zoomOutWeight * (velocity.magnitude - zoomVelocityLowThreshold);
        }
        
        return pos.y;
    }
}
