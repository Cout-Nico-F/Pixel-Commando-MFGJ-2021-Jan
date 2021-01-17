using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Threading;
=======
>>>>>>> main
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject deathPrefab;
    Animation hitAnimation;
    public int healthPoints = 100;
<<<<<<< HEAD
<<<<<<< HEAD
    AudioManager audioManager;

    
=======
=======
>>>>>>> main

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
<<<<<<< HEAD
>>>>>>> b1957a8545918e64e7ad95de4042bcc279b04e72
=======
>>>>>>> main
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
<<<<<<< HEAD
            UpdateAudioManager();
=======
>>>>>>> main
            healthPoints -= collision.GetComponent<Bulleting>().damage;
            hitAnimation.Play();
        }
    }

    private void Awake()
    {
        hitAnimation = GetComponent<Animation>();
<<<<<<< HEAD
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
=======
>>>>>>> main
    }

    private void Update()
    {
<<<<<<< HEAD

=======
>>>>>>> main
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathPrefab, this.transform.position, this.transform.rotation);
<<<<<<< HEAD
            if (this.gameObject.CompareTag("InfantryEnemy"))
            {
                audioManager.PlaySound("EnemySoldierDeath");
            }else if (this.gameObject.CompareTag("MachinegunEnemy"))
            {
                audioManager.PlaySound("EnemyMachineGunnerDeath");
            }

        }
    }

    void UpdateAudioManager()
    {

        if (healthPoints <= 100)
        {
            audioManager.PitchVariation = 1f;
        }
        else if (healthPoints == 67)
        {
            audioManager.PitchVariation = 1.3f;
        }
        else if (healthPoints == 34)
        {
            audioManager.PitchVariation = 1.7f;
        }
        else if (healthPoints <= 0)
        {
            audioManager.PitchVariation = 2f;
=======
>>>>>>> main
        }

        if (ooga1.ooga == true)
        {
            agent.SetDestination(target.position);
        }
    }
}
