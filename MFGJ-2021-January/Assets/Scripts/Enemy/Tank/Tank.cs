using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    //Get reference to all sub tank scripts
    [SerializeField]
    internal TankHealth healthScipt;
    [SerializeField]
    internal TankMovement movementScript;
    [SerializeField]
    internal TankAttack attackScript;
    [SerializeField]
    internal TankCollision collisionScript;

    [Header("Health")]  
    [SerializeField] internal int healthPoints;
    internal int maxhealthPoints = 100;

    [Header("Movement")]
    [SerializeField] internal float speed = 5.0f;
    [SerializeField] internal bool isFlipped = false;
    [SerializeField] internal Transform[] patrolPoints;
    [SerializeField] internal int currentPoint;

    [Header("Attack")]
    [SerializeField] internal int damage = 10;
    [SerializeField] internal float attackminRange = 10.0f;

    internal Animator anim;
    internal SpriteRenderer render;
    internal Rigidbody2D rb;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        healthPoints = maxhealthPoints;
    }
}
