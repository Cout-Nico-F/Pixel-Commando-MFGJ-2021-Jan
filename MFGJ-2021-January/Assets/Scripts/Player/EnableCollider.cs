using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{
    private CircleCollider2D myCollider;

    // Start is called before the first frame update
    private void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();

        Collider2D[] hitColliders;
        hitColliders = Physics2D.OverlapCircleAll(transform.position, myCollider.radius);

        for (int i = hitColliders.Length - 1; i > -1; i--)
        {
            EnableObject(hitColliders[i], true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnableObject(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnableObject(other, false);
    }

    private void EnableObject(Collider2D other, bool state)
    {
        if (other.CompareTag("InfantryEnemy") || other.CompareTag("MachinegunEnemy") || other.CompareTag("Hut"))
        {
            other.GetComponent<Enemy>().enabled = state;
            other.GetComponent<EnemyShooting>().enabled = state;
            other.GetComponent<SpriteRenderer>().enabled = state;
            other.GetComponent<Animator>().enabled = state;
            other.GetComponent<Animation>().enabled = state;
        }
    }
}
