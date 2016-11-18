using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject follow;
    public float cameraY;

	void Start () {
	    
	}
	
	void Update () {
	    if(follow != null)
        {
            Vector3 pos = follow.transform.position;
            pos.y = cameraY;
            transform.position = pos;
        }
	}
}
