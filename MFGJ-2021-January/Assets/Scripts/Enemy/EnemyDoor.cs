using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    Enemy enemy;

    [SerializeField]
    GameObject roof;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.healthPoints <= 0)
        {
            //remove Roof
            if (roof.activeSelf)
            {
                if (roof.GetComponent<OverlapBox>().HitColliders.Length > 0)
                {
                    ActivateEnemiesInside(roof.GetComponent<OverlapBox>().HitColliders);
                }
                roof.SetActive(false);
            }
        }
    }

    private void ActivateEnemiesInside(Collider2D[] hitColliders)
    {
        //Debug.Log("Colliders found: " + hitColliders.Length);

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("IndoorEnemy"))
            {
                var state = true;

                collider.GetComponent<Enemy>().enabled = state;
                collider.GetComponent<SpriteRenderer>().enabled = state;

                EnemyShooting[] enemyShootings = collider.GetComponents<EnemyShooting>();
                if (enemyShootings.Length > 1)
                {
                    for (int i = 0; i < enemyShootings.Length; i++)
                    {
                        enemyShootings[i].enabled = state;
                    }
                }
                else
                {
                    if (collider.GetComponent<EnemyShooting>() != null)
                    {
                        collider.GetComponent<EnemyShooting>().enabled = state;
                    }
                }

                if (collider.GetComponent<EnemyPatrol>() != null)
                {
                    collider.GetComponent<EnemyPatrol>().enabled = state;
                }

                if (collider.GetComponent<AnimatorUpdater>() != null)
                {
                    collider.GetComponent<AnimatorUpdater>().enabled = state;
                }

                if (collider.GetComponent<Animator>() != null)
                {
                    collider.GetComponent<Animator>().enabled = state;
                }

                if (collider.GetComponent<Animation>())
                {
                    collider.GetComponent<Animation>().enabled = state;
                }

                collider.tag = "InfantryEnemy"; //this allows them to be detected and affected by enableCollider again.
            }
        }
    }
}
