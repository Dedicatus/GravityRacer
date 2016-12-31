using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleVehicle : VehicleSuper {

    public float rotateSpeed;
    public float rotateAngle;

    bool turning = false;

    public override void OnRotateLeft() {
        turning = true;
        if (transform.rotation.eulerAngles.z > rotateAngle && transform.rotation.eulerAngles.z < 180) return;
        //print(transform.localEulerAngles);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    public override void OnRotateRight()
    {
        turning = true;
        if (transform.rotation.eulerAngles.z < 360 - rotateAngle && transform.rotation.eulerAngles.z > 180) return;
       // print(transform.localEulerAngles);
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
    }

    public override void OnHoldBoth() { }

    void Update()
    {
        if(turning)
        {
            turning = false;
            return;
        }
        if (transform.rotation.eulerAngles.z <= rotateSpeed * Time.deltaTime || transform.rotation.eulerAngles.z >= 360 - rotateSpeed * Time.deltaTime)
            transform.Rotate(0,0, -transform.rotation.eulerAngles.z);
        else if (transform.rotation.eulerAngles.z > rotateSpeed * Time.deltaTime && transform.rotation.eulerAngles.z < 180)
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        else if (transform.rotation.eulerAngles.z < 360 - rotateSpeed * Time.deltaTime && transform.rotation.eulerAngles.z > 180)
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

    }
}
