using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHullMovement : MonoBehaviour
{

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float rotationSpeed;

    private float speed;

    private float horizontal;
    private float vertical;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        if(vertical > 0)
        {
            speed = forwardSpeed;
        }
        else
        {
            speed = reverseSpeed;
        }


        rb.velocity = transform.forward * vertical * speed * Time.deltaTime;

        if(vertical >= 0)
        {
            transform.Rotate(0.0f, horizontal * rotationSpeed * Time.deltaTime, 0.0f);
        }
        else
        {
            transform.Rotate(0.0f, horizontal * -rotationSpeed * Time.deltaTime, 0.0f);
        }

    }


    private void OldMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 newPosition = new Vector3(horizontal, 0.0f, vertical);

        transform.LookAt(newPosition + transform.position);

        transform.Translate(newPosition* speed * Time.deltaTime, Space.World);
}

}
