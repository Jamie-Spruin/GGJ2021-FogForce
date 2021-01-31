using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Mover
{
    public int health = 1;
    public int points = 50;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    public void InflictDamage()
    {
        health--;

        if(health == 0)
        {
            var explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            GameManager.IncreaseScore(points);
            GameManager.gameManager.audioSource.PlayOneShot(explosionSound);
            Destroy(gameObject);
        }
    }
}
