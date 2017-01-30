using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum PlayerState
    {
        Paused,
        Launching,
        Playing,
        Dead
    }



    public static Player current;
    
    public PlayerState playerState;

    public float launchForce;

    public VehicleSuper vehicle;
    public PlayerPhysics physics;

    bool died;
    
    Rigidbody rigidBody;

    void Start () {
        playerState = PlayerState.Paused;
        died = false;
        current = this;
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if(playerState == PlayerState.Launching)
        {
        }
	}

	void FixedUpdate() {
        if(playerState == PlayerState.Playing || playerState == PlayerState.Dead)
        {
            //rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * force);
        } else if(playerState == PlayerState.Launching)
        {
            //rigidBody.AddForce(Vector3.down * gravity * rigidBody.mass);
        }
    }

    public void Launch()
    {
        playerState = PlayerState.Launching;
        physics.playerPhysicsState = PlayerPhysics.PlayerPhysicsState.Dropping;

        GetComponent<Rigidbody>().useGravity = true;
        rigidBody.AddForce(transform.localToWorldMatrix * Vector3.forward * launchForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(playerState == PlayerState.Launching)
        {
            playerState = PlayerState.Playing;
            physics.RegularPush();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (playerState == PlayerState.Launching)
        {
            playerState = PlayerState.Playing;
        }
    }

    public void hitGround()
    {
        if (playerState == PlayerState.Launching)
        {
            playerState = PlayerState.Playing;
        }
    }

    public void Accelerate()
    {
        if (playerState == PlayerState.Playing)
        {
            physics.AcceleratePush();
            vehicle.OnHoldBoth();
        }
    }

    public void Recover()
    {
        if (playerState == PlayerState.Playing)
        {
            physics.RegularPush();
        }
    }

    public void RotateLeft()
    {
        if (playerState == PlayerState.Playing)
        {
            physics.RotateLeft();
            vehicle.OnRotateLeft();
        }

    }

    public void RotateRight()
    {
        if (playerState == PlayerState.Playing)
        {
            physics.RotateRight();
            vehicle.OnRotateRight();
        }
    }

    public void Die()
    {
		if (playerState == PlayerState.Dead) return;
        playerState = PlayerState.Dead;
        if (died == false)
            GameManager.current.ReloadAfterDelay(2.0f);
        died = true;
        rigidBody.constraints = 0;
        rigidBody.AddTorque(new Vector3(1, 1, 1), ForceMode.Impulse);
		GameManager.current.SetHighScore ();
    }

}
