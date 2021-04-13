using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : IEnemyState
{
    private Enemy enemy;

    private float throwTimer;
    private float throwWait = 3;
    private bool canThrow = true;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowAttack();
        
        if(!enemy.InAttackRange)
        {
            enemy.ChangeEnemyState(new PatrolState());
        }
        else if(enemy.Target != null)
        {
            enemy.Movement();
        }
       
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

   private void ThrowAttack()
    {
        throwTimer += Time.deltaTime;
        if(throwTimer >= throwWait)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnim.SetTrigger("Attack");
        }
    }
}
