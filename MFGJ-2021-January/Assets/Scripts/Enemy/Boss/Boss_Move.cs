using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    Transform player;
    Transform trans;
    Rigidbody2D rb;
    Boss boss;
    Boss_Attack boss_attack;

    [Header("Variables")]
    public float speed = 2.5f;
    public float attackMinRange = 8;
    public float attackMaxRange = 20;

    [Header("Patrol Points")]
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    int current;
    [SerializeField]
    int randomPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        trans = animator.GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        boss_attack = animator.GetComponent<Boss_Attack>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*FOLLOW PLAYER 
        //boss.LookAtPlayer();
        //Vector2 target = new Vector2(player.position.x, player.position.y);
        //Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        //rb.MovePosition(newPos);*/

        //Only run one time -> Assign transforms of patrolPoints
        int repeat = 0;
        if (repeat==0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                patrolPoints[i] = GameObject.Find("Point (" + i + ")").GetComponent<Transform>();
            }
            repeat++; //No repeat more
        }

        //Patrol AI
        boss.LookAtPlayer();
        if (this.trans.position != patrolPoints[current].position)
        {
            trans.position = Vector3.MoveTowards(trans.position, patrolPoints[current].position, speed * Time.deltaTime);
        }
        else
        {
            //Get a random number -> random point
            if(randomPoint != current)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                current = randomPoint;
                //If the new numer is the same of the current number, get another one.
                if (randomPoint == current)
                {
                    randomPoint = Random.Range(0, patrolPoints.Length);
                    current = randomPoint;
                }
            }
            else
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                current = randomPoint;
            }

            //Get next point (in order of patrolPoitns list) 
            //current = (current + 1) % patrolPoints.Length;
        }


        //Change State to Attack One     
        if(Vector2.Distance(player.position, rb.position) <= attackMinRange) //If palyer is close
        {
            //First Attack
            animator.SetTrigger("AttackOne");
            Boss_Attack.attackNumber = 1;
        }
        else if (Vector2.Distance(player.position, rb.position) <= attackMaxRange &&
            Vector2.Distance(player.position, rb.position) > attackMinRange) //If player is not close
        {
            //Second Attack
            animator.SetTrigger("AttackTwo");
            Boss_Attack.attackNumber = 2;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackOne");
        animator.ResetTrigger("AttackTwo");
    }
}
