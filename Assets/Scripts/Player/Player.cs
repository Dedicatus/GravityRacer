using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player current;

    public float pushForce;
    public float forceAngle;

    Rigidbody rigidbody;

    void Start () {
        current = this;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        float forceRad = forceAngle * Mathf.Deg2Rad;
        transform.rotation = Quaternion.Euler(0, forceAngle, 0);
        rigidbody.AddForce(new Vector3(Mathf.Sin(forceRad), 0, Mathf.Cos(forceRad)) * pushForce);
	}
}
