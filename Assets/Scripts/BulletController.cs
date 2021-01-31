using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Mover
{
    public Vector3 movementSpeed;

    public float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
        
    }

    protected override void Update()
    {
        movementVector = movementSpeed;
        base.Update();

        if(Time.time - startTime > 10) // if bullet leaves bounds, make sure it doesn't last forever
        {
            RemoveBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyController>() != null && gameObject.layer == 10)
        {
            other.gameObject.GetComponent<EnemyController>().InflictDamage();
        }
        else if(other.gameObject.GetComponent<PlayerController>() != null && gameObject.layer == 12)
        {

        }

        if(other.gameObject.layer == 8 || other.gameObject.layer == 11 || other.gameObject.layer == 12)
        {
            RemoveBullet();
        }
    }

    private void RemoveBullet()
    {
        Destroy(gameObject); // pooling if I have time
        //gameObject.SetActive(false);
    }
}
