using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    float moveSpeed;
    public float spinSpeed;
    float spinDirection;

    public Rigidbody2D rb;
    private Rigidbody2D playerPos;
    public GameObject explosion;

    public SpriteRenderer sr;

    private void Start()
    {
        if (GameManager.Instance.player == null)
        {
            return;
        }

        health = GameManager.Instance.enemyHealth;
        moveSpeed = GameManager.Instance.enemySpeed;

        playerPos = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        spinDirection = Random.Range(0, 2) * 2 - 1;
    }

    private void Update()
    {
        transform.Rotate(spinSpeed * spinDirection * Vector3.forward * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (playerPos == null)
        {
            return;
        }

        Vector2 angle = (playerPos.position - rb.position).normalized;
        rb.MovePosition(rb.position + angle * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            StartCoroutine(DamageColor());

            if (health == 0)
            {
                GameManager.Instance.EnemyKilled();
                AudioManager.Instance.PlaySound(3);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageColor()
    {
        sr.color = new Color32(200, 0, 0, 255);
        yield return new WaitForSeconds(.2f);
        sr.color = Color.red; 
    }
}
