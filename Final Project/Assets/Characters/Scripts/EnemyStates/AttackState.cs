using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;
    private float attackTimer;
    private float attackWait = 2;
    private bool canAttack = true;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        AttackPlayer();
      
       if (!enemy.InAttackRange)
        {
           
            enemy.ChangeEnemyState(new PatrolState());

        }
        
       
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }

    private void AttackPlayer()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackWait)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnim.SetTrigger("Attack");
        }
    } 
}
