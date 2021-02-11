using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    Boss_Attack bossAttack;

    [Header("Variables")]
    public float speed = 2.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossAttack = animator.GetBehaviour<Boss_Attack>();

        for (int i = 0; i < boss.patrolPoints.Length; i++)
        {
            boss.patrolPoints[i] = GameObject.Find("Point (" + i + ")").GetComponent<Transform>();
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Look at Player
        boss.LookAtPlayer();

        //Change Move State to Attack One     
        if (Vector2.Distance(player.position, rb.position) <= bossAttack.attackMinRange) //If player is close
        {
            //First Attack
            animator.SetTrigger("AttackOne");
            Boss_Attack.attackNumber = 1;
        }
        else if (Vector2.Distance(player.position, rb.position) <= bossAttack.attackMaxRange &&
            Vector2.Distance(player.position, rb.position) > bossAttack.attackMinRange) //If player is not close
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
