using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject deathPrefab;
    Animation hitAnimation;
    public int healthPoints = 100;

    public Transform target;

    NavMeshAgent agent;

     public VariableController ooga1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GameObject g = GameObject.FindGameObjectWithTag("VariableController");
        ooga1 = g.GetComponent<VariableController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            healthPoints -= collision.GetComponent<Bulleting>().damage;
            hitAnimation.Play();
        }
    }

    private void Awake()
    {
        hitAnimation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
        }

        if (ooga1.ooga == true)
        {
            agent.SetDestination(target.position);
        }
    }
}
