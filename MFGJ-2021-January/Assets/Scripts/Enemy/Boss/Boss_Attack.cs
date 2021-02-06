using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : StateMachineBehaviour
{
    Boss boss;
    public static int attackNumber = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Call Attack Funtion
        if(attackNumber == 1)
        {
            //Call First Attack
            boss.FirstAttack();
            Debug.Log("ATACANDO #1");
        }
        else if (attackNumber == 2)
        {
            //Call Second Attack
            boss.SecondAttack();
            Debug.Log("ATACANDO #2");
        }

        //Change SecondAttack if boss health < 50
        if (boss.healthPoints < (boss.healthPoints / 2))
        {
            //Increade Attacj=k Two Damage
            boss.attackTwoDamage *= 2;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackNumber = 0;
    }
}
