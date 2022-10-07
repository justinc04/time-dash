using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public GameObject playerExplosion;

    public float time;
    public float enemyDamage;
    public float enemyReward;
    public int enemiesKilled;
    public int clockReward;
    public int superClockReward;
    public float lightningDuration;

    public int score;
    public int enemyScore;
    public int clockScore;
    public int lightningScore;
    public int superClockScore;

    public int enemyHealth;
    public float enemySpeed;

    bool gameEnded;
    bool startTime;
    public bool canChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void StartGame()
    {
        startTime = true;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void Update()
    {
        if (!startTime)
        {
            return;
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (!gameEnded)
        {
            gameEnded = true;
            time = -1;
            Instantiate(playerExplosion, player.transform.position, Quaternion.identity);
            Destroy(player);
            AudioManager.Instance.PlaySound(3);
            Invoke("EndGame", .5f);
        }
    }

    void EndGame()
    {
        UIManager.Instance.GameOver();
    }

    public void TakeDamage()
    {
        time -= enemyDamage;
    }

    public void EnemyKilled()
    {
        canChange = true;
        enemiesKilled++;
        time += enemyReward;
        score += enemyScore;

        if (enemiesKilled >= 15 && enemiesKilled % 15 == 0)
        {
            enemySpeed += .25f;
        }

        if (enemiesKilled >= 50 && enemiesKilled % 50 == 0)
        {
            enemyHealth++;
        }
    }

    public void ClockCollected()
    {
        time += clockReward;
        score += clockScore;
    }

    public void SuperClockCollected()
    {
        time += superClockReward;
        score += superClockScore;
    }

    public void LightningCollected()
    {
        score += lightningScore;
    }
}
