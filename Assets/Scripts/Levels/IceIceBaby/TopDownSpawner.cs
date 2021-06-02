using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownSpawner : MonoBehaviour
{
    //public Transform trLevel;
    public Camera levelCamera;
    public SpawnedEnemy[] prefabs;

    private const float timeBetweenSpawn = 1f;

    private float timeToSpawn;
    private float spawnRadius;
    private float halfCamHeight;
    private float halfCamWidth;

    private void Start()
    {
        spawnRadius = levelCamera.orthographicSize * 2.1f;
        halfCamHeight = levelCamera.orthographicSize;
        halfCamWidth = halfCamHeight * levelCamera.aspect;

    }

    private void Update()
    {
        if (GameManager.Instance.paused) return;

        if (timeToSpawn <= 0)
        {
            SpawnEnemy();
            timeToSpawn = timeBetweenSpawn;
        }
        else
        {
            timeToSpawn -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, prefabs.Length);
        Vector2 enemyPos = new Vector2();
        
        if(prefabs[index].spawnFromTop && prefabs[index].spawnFromBottom && prefabs[index].spawnFromLeft && prefabs[index].spawnFromRight)
        {
            Debug.Log("spawn from all");
        }
        else if (prefabs[index].spawnFromTop && prefabs[index].spawnFromBottom)
        {
            Debug.Log("from top and bottom");
        }
        else if (prefabs[index].spawnFromTop)
        {
            enemyPos = new Vector2(Random.Range(-halfCamWidth, halfCamWidth), halfCamHeight + prefabs[index].spriteHeight);
        }
        else if (prefabs[index].spawnFromBottom)
        {
            enemyPos = new Vector2(Random.Range(-halfCamWidth, halfCamWidth), -halfCamHeight - prefabs[index].spriteHeight);
        }
        else if (prefabs[index].spawnFromLeft && prefabs[index].spawnFromRight)
        {
        }
        else if (prefabs[index].spawnFromLeft)
        {
            enemyPos = new Vector2(-halfCamWidth - prefabs[index].spriteWidth, Random.Range(-halfCamHeight, halfCamHeight));

        }
        else if (prefabs[index].spawnFromRight)
        {
            enemyPos = new Vector2(halfCamWidth + prefabs[index].spriteWidth, Random.Range(-halfCamHeight, halfCamHeight));

        }

        Instantiate(prefabs[index], enemyPos, Quaternion.identity, parent: transform);
    }
}
