using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUpdater : MonoBehaviour
{
    private EnemyPatrol enemyPatrol;
    private Enemy enemy;
    private Vector3 directionFacing;
    private Transform player;
    public Animator animator;


    private void Awake()
    {
        if (GetComponent<EnemyPatrol>() != null)
        {
            enemyPatrol = GetComponent<EnemyPatrol>();
        }
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (!enemy.LevelManager.IsGameOver && Time.timeScale != 0)
        {
            if (enemy.IsDead == false && player != null)
            {
                UpdateAnimator();
            }
        }
    }

    private void UpdateAnimator()
    {
        if (enemyPatrol != null)
        {
            if (enemyPatrol.Patrolling)
            {
                directionFacing = (enemyPatrol.PatrolTarget - transform.position).normalized;
            }
            else directionFacing = (player.position - transform.position).normalized;
        }
        else directionFacing = (player.position - transform.position).normalized;

        if ((gameObject.CompareTag("InfantryEnemy") || gameObject.CompareTag("MachinegunEnemy")) && directionFacing.sqrMagnitude > 0 || gameObject.CompareTag("IndoorEnemy"))
        {
            animator.SetFloat("Horizontal", directionFacing.x);
            animator.SetFloat("Vertical", directionFacing.y);
            animator.SetFloat("Speed", directionFacing.sqrMagnitude);
        }
    }
}
