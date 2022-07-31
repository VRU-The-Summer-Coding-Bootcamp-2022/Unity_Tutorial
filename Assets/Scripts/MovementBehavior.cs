using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float xinput;
    private float yinput;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        var new_velocity = Vector3.ClampMagnitude(new Vector3(xinput, 0, yinput) * speed, speed);
        new_velocity = new Vector3(new_velocity.x, rb.velocity.y, new_velocity.z);
        rb.velocity = new_velocity;
    }

}
