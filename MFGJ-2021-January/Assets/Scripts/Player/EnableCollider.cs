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
        if (other.CompareTag("IndoorEnemy") == false)
        {
            EnableObject(other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("IndoorEnemy") == false)
        {
            EnableObject(other, false);
        }
    }

    private void EnableObject(Collider2D other, bool state)
    {
        //if (other.CompareTag("InfantryEnemy") || other.CompareTag("MachinegunEnemy") || other.CompareTag("Hut"))
        //{
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().enabled = state;
        }
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            other.GetComponent<SpriteRenderer>().enabled = state;
        }

        EnemyShooting[] enemyShootings = other.GetComponents<EnemyShooting>();
        if (enemyShootings.Length > 1)
        {
            for (int i = 0; i < enemyShootings.Length; i++)
            {
                enemyShootings[i].enabled = state;
            }
        }
        else
        {
            if (other.GetComponent<EnemyShooting>() != null)
            {
                other.GetComponent<EnemyShooting>().enabled = state;
            }
        }
        



        if (other.GetComponent<EnemyPatrol>() != null)
        {
            other.GetComponent<EnemyPatrol>().enabled = state;
        }

        if (other.GetComponent<AnimatorUpdater>() != null)
        {
            other.GetComponent<AnimatorUpdater>().enabled = state;
        }

        if (other.GetComponent<Animator>() != null)
        {
            other.GetComponent<Animator>().enabled = state;
        }

        if (other.GetComponent<Animation>())
        {
            other.GetComponent<Animation>().enabled = state;
        }
        //}
    }
}
