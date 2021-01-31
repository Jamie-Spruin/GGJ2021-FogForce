﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    public float moveSpeed = 20;
    public float leanAngle = 15;
    public int bulletSpeed = 32;
    public float leanTransitionMultiplier = 2; // < 1 is slow and > 1 is fast
    public Transform modelRoot;
    public AudioClip bulletFire;
    public float bulletWaitTime = 0.5f;
    public Camera rendercam;
    public GameObject bulletPrefab;

    public CutoutMover mouseCutout;
    public CutoutMover shipCutout;

    private Vector2 moveInput;
    private float lastFireTime = int.MinValue;

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

        if(Input.GetMouseButton(0))
        {
            if(Time.time - lastFireTime > bulletWaitTime)
            {
                FireBullet();
            }
        }

        var shipTarget = rendercam.WorldToScreenPoint(transform.position) - new Vector3(GameManager.GAME_WIDTH / 2f, GameManager.GAME_HEIGHT / 2f);
        //shipTarget /= (float)Screen.height / (float)GameManager.GAME_HEIGHT;

        shipTarget.z = 0;
        shipCutout.Move(shipTarget);
        Vector2 mouseTarget = Input.mousePosition - new Vector3(Screen.width, Screen.height, 0) / 2;
        mouseCutout.Move(mouseTarget / ((float)Screen.height / (float)GameManager.GAME_HEIGHT));
    }

    public void InflictDamage()
    {

    }

    public void FireBullet()
    {
        lastFireTime = Time.time;
        GameManager.gameManager.audioSource.PlayOneShot(bulletFire);
        Debug.Log(rendercam.pixelHeight);
        Vector3 target = rendercam.WorldToScreenPoint(transform.position) * ((float)Screen.height / (float)GameManager.GAME_HEIGHT);
        target.x = Input.mousePosition.x - target.x;
        target.z = Input.mousePosition.y - target.y;
        target.y = 0;

        var bullet = Instantiate(bulletPrefab).GetComponent<BulletController>(); // temp, will pool when time
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(target, Vector3.up);
        bullet.movementSpeed = target.normalized * bulletSpeed;

    }
}
