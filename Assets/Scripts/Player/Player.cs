using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotateSpeed;

    public static Player current;

    public float pushForce;
    public float gravity;

    bool died;
    
    Rigidbody rigidBody;

    void Start () {
        died = false;
        current = this;
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		
	}

	void FixedUpdate() {
        rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * pushForce);

        //gravity
        rigidBody.AddForce(transform.localToWorldMatrix * Vector3.down * gravity);

    }

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void Die()
    {
        CameraFollow.current.follow = null;
        died = true;
        rigidBody.constraints = 0;
        rigidBody.AddTorque(new Vector3(1, 1, 1), ForceMode.Impulse);
    }
}
