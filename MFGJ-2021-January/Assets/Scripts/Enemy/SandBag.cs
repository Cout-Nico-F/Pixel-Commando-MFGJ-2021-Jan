using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    public float blockProbability = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            float result = Random.value;
            if (result < blockProbability)
            {
                Destroy(collision.gameObject);
                Resources.Load("Block") ;
            }
        }
    }
}
