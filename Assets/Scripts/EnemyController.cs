using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Mover
{
    public int health = 1;
    public int points = 50;
    public AudioClip explosionSound;
    public AudioClip shootSound;
    public GameObject bulletPrefab;
    public GameObject modelRoot;
    public void InflictDamage()
    {
        health--;

        GameManager.gameManager.audioSource.PlayOneShot(explosionSound);

        if(health == 0)
        {
            var explosion = Instantiate(GameManager.gameManager.explosionPrefab);
            explosion.transform.position = transform.position;
            GameManager.IncreaseScore(points);
            Destroy(gameObject);
        }
    }

    public void FireBullet(Vector3 direction, float speed)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;

        var bulletController = bullet.GetComponent<BulletController>();
        bulletController.movementSpeed = direction.normalized * speed;

        GameManager.gameManager.audioSource.PlayOneShot(shootSound);
    }

    public Vector3 GetPlayerTarget()
    {
        return (GameManager.gameManager.player.position - transform.position).normalized;
    }
}
