using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicForwardShootTurn : EnemyController
{
    public float speed = 8;

    public float targetDistance = 24;
    public float turnAcceleration = 12f;
    private float direction; // -1 or 1 - used for if it should go left or right
    public float bulletInterval = 0.4f;
    public float bulletCount = 3;
    public float bulletSpeed = 52;
    public float rotationsPerSecond = 360;

    private bool isTurning = false;
    private float xSpeed = 0;
    private float lastBulletTime;

    // Update is called once per frame
    protected override void Update()
    {
        if (transform.position.z < targetDistance && !isTurning)
        {
            isTurning = true;
            if (bulletCount > 0)
            {
                FireBullet(GetPlayerTarget(), bulletSpeed);
                lastBulletTime = Time.time;
                bulletCount--;
            }
            if (transform.position.x > 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }

        if (isTurning)
        {
            modelRoot.transform.Rotate(0, 0, -rotationsPerSecond * direction * Time.deltaTime);
            xSpeed += turnAcceleration * direction * Time.deltaTime;

            if(Time.time - lastBulletTime > bulletInterval && bulletCount > 0)
            {
                FireBullet(GetPlayerTarget(), bulletSpeed);
                lastBulletTime = Time.time;
                bulletCount--;
            }
        }

        movementVector = Vector3.back * speed + Vector3.right * xSpeed;

        base.Update();
    }
}
