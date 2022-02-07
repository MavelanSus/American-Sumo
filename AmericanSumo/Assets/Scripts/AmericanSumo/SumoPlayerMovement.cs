using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoPlayerMovement : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        float h = -fixedJoystick.Horizontal;
        float v = fixedJoystick.Vertical;

        if (v != 0 || h != 0)
        {
            Vector3 direction = (Vector3.forward * h) + (Vector3.right * v);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
