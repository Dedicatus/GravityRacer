using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour {

    public enum PlayerPhysicsState
    {
        Paused,
        RegularMoving,
        Accelerating,
        Dropping
    }

    public float pushForce;
    public float accelerateForce;
    public float gravity;
    public float rotateSpeed;
    public float force;


    Rigidbody rigidBody;

    [HideInInspector]
    public PlayerPhysicsState playerPhysicsState;
    [HideInInspector]
    public int collisionCount;

    void Start () {
        playerPhysicsState = PlayerPhysicsState.Paused;
        rigidBody = GetComponent<Rigidbody>();
    }
	
	void Update () {
		
	}

    void FixedUpdate()
    {
        if(playerPhysicsState == PlayerPhysicsState.RegularMoving)
        {
            rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * pushForce);
            if (collisionCount == 0)
                rigidBody.AddForce(Vector3.down * gravity * rigidBody.mass);
        }
        else if (playerPhysicsState == PlayerPhysicsState.Accelerating)
        {
            rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * accelerateForce);
            if (collisionCount == 0)
                rigidBody.AddForce(Vector3.down * gravity * rigidBody.mass);
        }
        else if (playerPhysicsState == PlayerPhysicsState.Dropping)
        {
            if (collisionCount == 0)
                rigidBody.AddForce(Vector3.down * gravity * rigidBody.mass);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
    }

    void OnCollisionExit(Collision collision)
    {
        collisionCount--;
    }

    public virtual void RegularPush()
    {
        playerPhysicsState = PlayerPhysicsState.RegularMoving;
    }

    public virtual void AcceleratePush()
    {
        playerPhysicsState = PlayerPhysicsState.Accelerating;
    }

    public virtual void RotateLeft()
    {
        transform.Rotate(transform.worldToLocalMatrix * Vector3.up, -rotateSpeed * Time.fixedDeltaTime);
    }

    public virtual void RotateRight()
    {
        transform.Rotate(transform.worldToLocalMatrix * Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }
}
