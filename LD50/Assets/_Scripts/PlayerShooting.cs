using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float reloadTime;
    public float fastReloadTime;

    bool canShoot = true;
    public bool sprinting;

    private void Update()
    {
        if (canShoot && Input.GetButton("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySound(0);

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        canShoot = false;

        float time = sprinting ? fastReloadTime : reloadTime;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
