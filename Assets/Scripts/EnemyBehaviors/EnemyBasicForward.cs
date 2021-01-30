using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicForward : EnemyController
{
    public float speed = 8;

    // Update is called once per frame
    protected override void Update()
    {
        movementVector = Vector3.back * speed;

        base.Update();
    }
}
