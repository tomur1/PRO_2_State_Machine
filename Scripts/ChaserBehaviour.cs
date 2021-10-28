using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBehaviour : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb2d;
    public float speed = 5;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = Vector2.Distance(rb2d.position, player.position);
        if (distanceToPlayer < 5)
        {
            Move();
            animator.SetBool("HeadRotate", true);
            if (distanceToPlayer < 1)
            {
                animator.SetBool("AttackPlayer", true);
            }
            else
            {
                animator.SetBool("AttackPlayer", false);
            }
        }
        else
        {
            animator.SetBool("HeadRotate", false);
        }
    }

    private void Move()
    {
        var target = new Vector2(player.position.x, player.position.y);
        var newPosition = Vector2.MoveTowards(rb2d.position, target, speed * Time.deltaTime);
        rb2d.MovePosition(newPosition);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
