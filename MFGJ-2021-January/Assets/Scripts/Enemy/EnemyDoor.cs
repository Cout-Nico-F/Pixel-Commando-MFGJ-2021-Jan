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
                ActivateEnemiesInside(roof.GetComponent<OverlapBox>().HitColliders);
                roof.SetActive(false);
            }
        }
    }

    private void ActivateEnemiesInside(Collider2D[] hitColliders)
    {
        Debug.Log("Colliders found: " + hitColliders.Length);

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("IndoorEnemy"))
            {
                var state = true;

                collider.GetComponent<Enemy>().enabled = state;
                collider.GetComponent<SpriteRenderer>().enabled = state;

                if (collider.GetComponent<EnemyShooting>() != null)
                {
                    collider.GetComponent<EnemyShooting>().enabled = state;
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
            }
        }
    }
}
