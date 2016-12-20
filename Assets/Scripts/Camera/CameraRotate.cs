using UnityEngine;
using System.Collections;

public enum CameraTrackWay
{
    trackHead,
    trackVelocity,
    trackPosition
}

public class CameraRotate : MonoBehaviour {

    public float rotateSpeed;

    public CameraTrackWay cameraTrackWay;

    void Start () {
	
	}

	void Update () {
        if (Player.current != null)
        {
            if(CameraTrackWay.trackHead == cameraTrackWay)
            {
                transform.rotation = Quaternion.Euler(30.0f, Player.current.transform.rotation.eulerAngles.y, 0);
            }
            else if (CameraTrackWay.trackVelocity == cameraTrackWay)
            {
                Vector3 velocityDir = Player.current.GetComponent<Rigidbody>().velocity.normalized;
                float angle = Vector3.Angle(Vector3.forward, velocityDir);
                if (velocityDir.x < 0) angle = -angle;
                Quaternion target = Quaternion.Euler(90.0f, angle, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed * Time.deltaTime);
            } else
            {

            }
        }
    }

    public void RotateLeft()
    {
        //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void RotateRight()
    {
        //transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
    }
}
