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

    AudioManager audioManager;

    private void Awake()
    {
        hitAnimation = GetComponent<Animation>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        try
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            GameObject g = GameObject.FindGameObjectWithTag("VariableController");
            ooga1 = g.GetComponent<VariableController>();
        } catch
        {
            Debug.Log("No NavMeshAgent in enemy");
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            healthPoints -= collision.GetComponent<Bulleting>().damage;
            hitAnimation.Play();
        }
    }

    

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
            if (this.gameObject.CompareTag("InfantryEnemy"))
            {
                audioManager.PlaySound("EnemySoldierDeath");
            }
            else if (this.gameObject.CompareTag("MachinegunEnemy"))
            {
                audioManager.PlaySound("EnemyMachineGunnerDeath");
            }
        }

       /*if (ooga1.ooga == true)
        {
            agent.SetDestination(target.position);
        }*/
    }
}
