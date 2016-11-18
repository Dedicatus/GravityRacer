using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public float rotateSpeed;
    
	void Start () {
	
	}

	void Update () {
        if (Player.current != null)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            }
            //print(transform.rotation.eulerAngles);
            Player.current.forceAngle = transform.rotation.eulerAngles.y;
        }
    }
}
