using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSuper : MonoBehaviour {

    public float tiltAngle;

    public virtual void OnRotateLeft() { }

    public virtual void OnRotateRight() { }

    public virtual void OnHoldBoth() { }
}
