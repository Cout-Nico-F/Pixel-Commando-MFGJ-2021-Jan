using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    public float blockProbability;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            float result = Random.value;
            if (result < blockProbability)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
