using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float enemySpawnTime;
    public float enemySpawnMultiplier;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public Vector2 mapSize;

    [Header("Powerups")]
    public float wallCheckRadius;
    public float playerCheckRadius;

    [Header("Clock")]
    public int initialClocks;
    public float clockCheckRadius;
    public float clockSpawnTime;
    public GameObject clockPrefab;

    [Header("Lightning")]
    public int initialLightnings;
    public float lightningCheckRadius;
    public float lightningSpawnTime;
    public GameObject lightningPrefab;

    [Header("Super Clock")]
    public int initialSuperClocks;
    public float superClockCheckRadius;
    public float superClockSpawnTime;
    public GameObject superClockPrefab;

    public LayerMask wallLayer;
    public LayerMask playerLayer;
    public LayerMask powerupLayer;


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups(clockPrefab, clockSpawnTime, clockCheckRadius));
        StartCoroutine(SpawnPowerups(lightningPrefab, lightningSpawnTime, lightningCheckRadius));
        StartCoroutine(SpawnPowerups(superClockPrefab, superClockSpawnTime, superClockCheckRadius));

        for (int i = 0; i < initialClocks; i++)
        {
            SpawnPowerup(clockPrefab, clockSpawnTime, clockCheckRadius);
        }

        for (int i = 0; i < initialLightnings; i++)
        {
            SpawnPowerup(lightningPrefab, lightningSpawnTime, lightningCheckRadius);
        }

        for (int i = 0; i < initialSuperClocks; i++)
        {
            SpawnPowerup(superClockPrefab, superClockSpawnTime, superClockCheckRadius);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.canChange && GameManager.Instance.enemiesKilled >= 25 && GameManager.Instance.enemiesKilled % 25 == 0)
        {
            GameManager.Instance.canChange = false;
            enemySpawnTime *= enemySpawnMultiplier;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerups(GameObject prefab, float spawnTime, float checkRadius)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnPowerup(prefab, spawnTime, checkRadius);
        }
    }

    void SpawnPowerup(GameObject prefab, float spawnTime, float checkRadius)
    {
        Vector2 spawnPos = RandomPosition();

        int checkIterations = 200;

        while (checkIterations > 0 && (Physics2D.OverlapCircle(spawnPos, wallCheckRadius, wallLayer) || Physics2D.OverlapCircle(spawnPos, playerCheckRadius, playerLayer) || Physics2D.OverlapCircle(spawnPos, checkRadius, powerupLayer)))
        {
            spawnPos = RandomPosition();
            checkIterations--;
        }

        if (checkIterations > 0)
        {
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    Vector2 RandomPosition()
    {
        float xPos = Random.Range(-mapSize.x * .5f, mapSize.x * .5f);
        float yPos = Random.Range(-mapSize.x * .5f, mapSize.x * .5f);
        return new Vector2(xPos, yPos);
    }
}
