using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    Boss_Attack boss_attack;

    [Header("Variables")]
    public float speed = 2.5f;
    public float attackMinRange = 8;
    public float attackMaxRange = 20;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        boss_attack = animator.GetComponent<Boss_Attack>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //FOLLOW PLAYER -> CHANGE ALL CODE AND CREATE A RANDOM MOVEMENT
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

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
