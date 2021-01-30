using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    public float moveSpeed = 20;
    public float leanAngle = 15;
    public float leanTransitionMultiplier = 2; // < 1 is slow and > 1 is fast
    public Transform modelRoot;

    private Vector2 moveInput;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        modelRoot.localRotation = Quaternion.Slerp(modelRoot.localRotation, Quaternion.Euler(0, 0, moveInput.x * -leanAngle), leanTransitionMultiplier * Time.deltaTime);

        moveInput = moveInput.normalized;

        movementVector = new Vector3(moveInput.x * moveSpeed, 0, moveInput.y * moveSpeed); // Deltatime handled in base

        // lazy rotation stuff - fine for jam but shouldn't do this

        base.Update(); // update movement and common mover stuff
    }
}
