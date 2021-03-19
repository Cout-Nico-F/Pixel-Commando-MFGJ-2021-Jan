using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    LevelManager levelManager;
    Transform player;

    //Patrol
    [Header("Patrol")]
    public float startWaitTime;
    public float randomStepSize;
    private Vector3 randomStep;
    private Vector3 patrolTarget;
    private bool patrolling = true;
    private float waitTime;
    public float stoppingDistance;
    public float retreatDistance;
    public float detectionRadius;
    public float moveSpeed;
    



    public bool Patrolling { get => patrolling;}
    public Vector3 PatrolTarget { get => patrolTarget;}

    // Start is called before the first frame update
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        waitTime = startWaitTime;
        randomStep = new Vector3(Random.Range(-randomStepSize, randomStepSize), Random.Range(-randomStepSize, randomStepSize), 0);
        patrolTarget = transform.position + randomStep;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.IsGameOver && Time.timeScale != 0)
        {
            MoveEnemy();
            Patrol();

            if (GameObject.FindGameObjectWithTag("Player") != null) //TODO: refactor here
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else player = null;
        }
    }
    private void MoveEnemy()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) < detectionRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                patrolling = false;
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }
            else patrolling = true;
        }
    }
    private void Patrol()
    {
        if (patrolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);

            if (waitTime <= 0)
            {
                randomStep = new Vector3(Random.Range(-randomStepSize, randomStepSize), Random.Range(-randomStepSize, randomStepSize), 0);
                patrolTarget = transform.position + randomStep;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
