using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public SpriteRenderer sr;
    public PlayerMovement movement;
    public PlayerShooting shooting;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.TakeDamage();
            StartCoroutine(DamageColor());
            AudioManager.Instance.PlaySound(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Clock")
        {
            GameManager.Instance.ClockCollected();
            Destroy(collision.gameObject);
            AudioManager.Instance.PlaySound(2);
        }
        else if (collision.gameObject.tag == "Lightning")
        {
            StartCoroutine(Sprint());
            GameManager.Instance.LightningCollected();
            Destroy(collision.gameObject);
            AudioManager.Instance.PlaySound(2);
        }
        else if (collision.gameObject.tag == "Super")
        {
            GameManager.Instance.SuperClockCollected();
            Destroy(collision.gameObject);
            AudioManager.Instance.PlaySound(2);
        }
    }

    IEnumerator DamageColor()
    {
        for (int i = 0; i < 6; i++)
        {
            sr.color = new Color32(200, 0, 0, 255);
            yield return new WaitForSeconds(.1f);
            sr.color = new Color32(0, 140, 255, 255);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator Sprint()
    {
        movement.sprinting = true;
        shooting.sprinting = true;
        yield return new WaitForSeconds(GameManager.Instance.lightningDuration);
        movement.sprinting = false;
        shooting.sprinting = false;
    }
}
