using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 movementVector;

    protected virtual void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    protected virtual void Update()
    {
        rb.MovePosition(rb.position + (movementVector * Time.deltaTime));
    }
}
