using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 2f;
    Transform player;
    Transform BossTran;
    Rigidbody2D bossrb;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossrb = animator.GetComponent<Rigidbody2D>();
        BossTran = GameObject.Find("Boss").transform;
        boss = animator.GetComponent<Boss>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss.FollowPlayer();
        bossrb.velocity = Vector2.left * speed;

        float xDir = player.position.x - BossTran.position.x;

        if (xDir < 0 )
        {
            bossrb.velocity = Vector2.left *speed;
            
        }
        else if (xDir > 0)
        {
            bossrb.velocity = Vector2.right * speed;
            
        }

       /* Vector2 target = new Vector2(player.position.x, bossrb.position.y);
      Vector2 newPos =  Vector2.MoveTowards(bossrb.position, target, speed * Time.fixedDeltaTime);
        bossrb.MovePosition(newPos);
       */
       if( Vector2.Distance(player.position,bossrb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
      
    }

}
