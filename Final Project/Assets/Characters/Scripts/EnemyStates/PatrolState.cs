using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDur;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        patrolDur = UnityEngine.Random.Range(1, 10);
    }

    public void Execute()
    {
       
        Patroling();
        enemy.Movement();

        if(enemy.Target !=  null && enemy.tag == "enemy" && enemy.InAttackRange)
        {
            enemy.ChangeEnemyState(new AttackState());
        }
        else if(enemy.Target != null && enemy.tag == "Enemy_throw" && enemy.InAttackRange)
        {
            enemy.ChangeEnemyState(new RangedAttackState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }
    private void Patroling()
    {
       
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDur)
        {
            enemy.ChangeEnemyState(new IdleState());
        }
    }

}
