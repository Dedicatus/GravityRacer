using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotateSpeed;

    public static Player current;

    public float pushForce;
    
    Rigidbody rigidBody;

    void Start () {
        current = this;
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		
	}

	void FixedUpdate() {
		rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * pushForce);
	}

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

}
