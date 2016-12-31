using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum PlayerState
    {
        Paused,
        Playing,
        Dead
    }


    public float rotateSpeed;

    public static Player current;

    public float pushForce;
    public float gravity;

    public PlayerState playerState;

    public VehicleSuper vehicle;

    bool died;
    
    Rigidbody rigidBody;

    void Start () {
        playerState = PlayerState.Paused;
        died = false;
        current = this;
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		
	}

	void FixedUpdate() {
        if(playerState == PlayerState.Playing || playerState == PlayerState.Dead)
        {
            rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * pushForce);
            rigidBody.AddForce(transform.localToWorldMatrix * Vector3.down * gravity);
        }

        //gravity

    }

    public void RotateLeft()
    {
        if (playerState == PlayerState.Playing)
        {
            transform.Rotate(transform.worldToLocalMatrix * Vector3.up, -rotateSpeed * Time.deltaTime);
            vehicle.OnRotateLeft();
        }

    }

    public void RotateRight()
    {
        if (playerState == PlayerState.Playing)
        {
            transform.Rotate(transform.worldToLocalMatrix * Vector3.up, rotateSpeed * Time.deltaTime);
            vehicle.OnRotateRight();
        }
    }

    public void Die()
    {
        if (playerState == PlayerState.Dead) return;
        playerState = PlayerState.Dead;
        CameraFollow.current.follow = null;
        if (died == false)
            GameManager.current.ReloadAfterDelay(2.0f);
        died = true;
        rigidBody.constraints = 0;
        rigidBody.AddTorque(new Vector3(1, 1, 1), ForceMode.Impulse);
    }

}
