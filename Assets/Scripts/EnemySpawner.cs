using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public float minZPosition = 60; // spawn point

    private float cooldown = float.MinValue;

    private System.Random rnd = new System.Random();

    public List<Action> spawnMethods = new List<Action>();

    public GameObject sturdyBasic;
    public GameObject disk;

    private float lastTime;
    //public GameObject spamDisk;

    // Start is called before the first frame update
    void Start()
    {
        spawnMethods.Add(SpawnSturdyRandom);
        spawnMethods.Add(SpawnDiskRandom);
        spawnMethods.Add(SpawnDiskParallel);
        spawnMethods.Add(SpawnDiskRandomLines);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > cooldown)
        {
            spawnMethods[rnd.Next(spawnMethods.Count)].Invoke();
            lastTime = Time.time;
        }
    }

    public void SpawnSturdyRandom()
    {
        cooldown = 3f;
        var spawns = rnd.Next(8, 12);
        for (int i = 0; i < spawns-1; i++)
        {
            var spawn = Instantiate(sturdyBasic);
            spawn.transform.position = GetRandomPosition(17, 28);
        }

        // Spawn one above the player to deter camping
        var playerTarget = Instantiate(sturdyBasic);
        playerTarget.transform.position = new Vector3(GameManager.gameManager.player.position.x, 0, minZPosition - 2);
    }

    public void SpawnDiskRandom()
    {
        cooldown = 4;
        var spawns = rnd.Next(3, 6);
        for (int i = 0; i < spawns; i++)
        {
            var spawn = Instantiate(disk);
            spawn.transform.position = GetRandomPosition(14,60);
        }
    }

    public void SpawnDiskParallel()
    {
        cooldown = 4;
        for (int i = 0; i < 3; i++)
        {
            var spawn = Instantiate(disk);
            spawn.transform.position = new Vector3(-12, 0, i * 12 + minZPosition);
            spawn = Instantiate(disk);
            spawn.transform.position = new Vector3(12, 0, i * 12 + minZPosition);
        }
    }

    public void SpawnDiskRandomLines()
    {
        cooldown = 6f;

        for (int j = 0; j < 4; j++)
        {
            var position = rnd.Next(-12, 12);

            for (int i = 0; i < 2; i++)
            {
                var spawn = Instantiate(disk);
                spawn.transform.position = new Vector3(position, 0, i * 6  + j * 24 + minZPosition);
            }
        }

        for (int i = 0; i < 6; i++)
        {
            var spawn = Instantiate(sturdyBasic);
            spawn.transform.position = GetRandomPosition(17, 60);
        }
    }

    public Vector3 GetRandomPosition(int x, int y)
    {
        return new Vector3(rnd.Next(-x, x), 0, rnd.Next((int)minZPosition, y + (int)minZPosition));
    }
}