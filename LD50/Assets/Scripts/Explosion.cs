using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Invoke("Die", 2f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
