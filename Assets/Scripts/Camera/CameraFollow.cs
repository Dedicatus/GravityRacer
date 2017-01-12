using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow current;

    public GameObject follow;
    public float cameraY;

    public CameraTrackWay cameraTrackWay;

    public Rigidbody carRigidBody;
    public Vector3 trackVelocityOffset;
    public float rotationThreshold = 1f;
    public float cameraStickiness = 10.0f;
    public float cameraRotationSpeed = 5.0f;

    public bool zoom;
    public float zoomVelocityLowThreshold;
    public float zoomOutWeight;

	void Start () {
        current = this;

    }
	
	void Update () {
	    if(follow != null && Player.current.playerState != Player.PlayerState.Dead)
        {
            CameraTrackWay trackWay = cameraTrackWay;//GetComponent<CameraRotate>().cameraTrackWay;
            if (trackWay == CameraTrackWay.trackHead)
            {
                Vector3 offset = follow.transform.localToWorldMatrix * Vector3.back * 15.0f;//(Quaternion.Euler(0, follow.transform.rotation.eulerAngles.y, 0) * Vector3.back * 40.0f);
                Vector3 pos = follow.transform.position + offset;
                if (zoom)
                    pos.y = pos.y + cameraY;//getZoomCameraY(); //cameraY;
                else
                    pos.y = pos.y + cameraY;
                transform.position = pos;
            }
            if(trackWay == CameraTrackWay.trackVelocity)
            {

                //Vector3 velocity = follow.GetComponent<Rigidbody>().velocity;
               // print(tiltFollow.tiltAngle);
                //gameObject.transform.forward = ((follow.transform.position + follow.transform.forward * 10.0f) - transform.position).normalized;
                //velocity.y = 0;
                //offset = velocity.normalized * -15.0f;
            }
        }
	}

    void FixedUpdate()
    {
        if (cameraTrackWay == CameraTrackWay.trackVelocity)
        {

            Quaternion look;

            // Moves the camera to match the car's position.
            Vector3 offset = (transform.localToWorldMatrix * trackVelocityOffset);
            transform.position = Vector3.Lerp(transform.position, follow.transform.position + offset, cameraStickiness * Time.fixedDeltaTime) ;

            // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
            if (carRigidBody.velocity.magnitude < rotationThreshold)
                look = Quaternion.LookRotation(follow.transform.forward);
            else
                look = Quaternion.LookRotation(carRigidBody.velocity.normalized);

            // Rotate the camera towards the velocity vector.
            look = Quaternion.Slerp(transform.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            transform.rotation = look;
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
